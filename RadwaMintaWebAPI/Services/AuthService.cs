using MailKit;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.DTOs.Authentication;
using RadwaMintaWebAPI.Helpers;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;
using System.Security.Cryptography;

namespace RadwaMintaWebAPI.Services
{
    public class AuthService(MintaDbContext _context, ITokenService _tokenService, ILogger<AuthService> _logger, IEmailService _emailService) : IAuthService
    {
        
        #region Login
        public async Task<ApiResponse<string>> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null || string.IsNullOrWhiteSpace(loginRequestDTO.Email) || string.IsNullOrWhiteSpace(loginRequestDTO.Password))
            {
                return ApiResponse<string>.FailureResponse("Email and password are required.");
            }

            var admin = await _context.Admins
                                     .FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email);

            if (admin == null || admin.Password != loginRequestDTO.Password) 
            {
                return ApiResponse<string>.FailureResponse("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(admin);
            return ApiResponse<string>.SuccessResponse(
           message: "Login Successfully!.",
           token: token
           );
        }

        #endregion

        #region Forgot Password

       
        public async Task<ApiResponse<bool>> ForgetPasswordRequestAsync(string email)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);
            if (admin == null)
            {
                return ApiResponse<bool>.SuccessResponse(message: "If an account exists for this email, an OTP has been sent.");
            }

            var otp = GenerateOtp();
            var otpExpirationMinutes = 15;
            var expirationTime = DateTime.UtcNow.AddMinutes(otpExpirationMinutes);

            var existingOtp = await _context.PasswordResetOtps.FirstOrDefaultAsync(o => o.Email == email && o.ExpirationTime > DateTime.UtcNow);
            if (existingOtp != null)
            {
                existingOtp.Otp = otp;
                existingOtp.ExpirationTime = expirationTime;
                existingOtp.CreatedTime = DateTime.UtcNow; 
            }
            else
            {
                _context.PasswordResetOtps.Add(new PasswordResetOtp
                {
                    Email = email,
                    Otp = otp,
                    ExpirationTime = expirationTime,
                    CreatedTime = DateTime.UtcNow 
                });
            }
            await _context.SaveChangesAsync();

            var subject = "Password Reset OTP";
            string htmlBody;
            string passwordResetTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordResetOtpTemplate.html");

            if (File.Exists(passwordResetTemplatePath))
            {
                htmlBody = await File.ReadAllTextAsync(passwordResetTemplatePath);
                htmlBody = htmlBody.Replace("{{OTP}}", otp);
                htmlBody = htmlBody.Replace("{{EXPIRATION_MINUTES}}", otpExpirationMinutes.ToString());
                htmlBody = htmlBody.Replace("{{CURRENT_YEAR}}", DateTime.UtcNow.Year.ToString());
                htmlBody = htmlBody.Replace("Your Company Name", "Radwa Minta Web API");
            }
            else
            {
                htmlBody = $"<p>Your One-Time Password (OTP) for password reset is: <strong>{otp}</strong></p><p>This OTP is valid for {otpExpirationMinutes} minutes.</p>";
                _logger.LogWarning($"PasswordResetOtpTemplate.html not found at {passwordResetTemplatePath}. Using default email body.");
            }

            var emailSent = await _emailService.SendEmailAsync(email, subject, htmlBody);

            if (emailSent)
            {
                return ApiResponse<bool>.SuccessResponse(message: "OTP sent to your email.");
            }
            else
            {
                if (existingOtp != null) _context.PasswordResetOtps.Remove(existingOtp);
                else _context.PasswordResetOtps.RemoveRange(_context.PasswordResetOtps.Where(o => o.Email == email && o.Otp == otp));
                await _context.SaveChangesAsync();
                _logger.LogError("Failed to send password reset OTP to {Email}", email);
                return ApiResponse<bool>.FailureResponse("Failed to send OTP. Please try again.");
            }
        }


       
        public async Task<ApiResponse<bool>> ResendOtpAsync(string email)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);
            if (admin == null)
            {
                return ApiResponse<bool>.SuccessResponse(message: "If an account exists for this email, a new OTP has been sent.");
            }

            var lastOtp = await _context.PasswordResetOtps
                                  .Where(o => o.Email == email)
                                  .OrderByDescending(o => o.CreatedTime)
                                  .FirstOrDefaultAsync();

            if (lastOtp != null && (DateTime.UtcNow - lastOtp.CreatedTime).TotalSeconds < 60) 
            {
                return ApiResponse<bool>.FailureResponse("Please wait before resending OTP.");
            }

            return await ForgetPasswordRequestAsync(email);
        }

        public async Task<ApiResponse<bool>> VerifyOtpAsync(string email, string otp)
        {
            var storedOtp = await _context.PasswordResetOtps
                                   .FirstOrDefaultAsync(o => o.Email == email && o.Otp == otp && o.ExpirationTime > DateTime.UtcNow);

            if (storedOtp == null)
            {
                _logger.LogWarning("OTP verification failed for email {Email}. Either OTP is incorrect or expired.", email);
                return ApiResponse<bool>.FailureResponse("Invalid or expired OTP.");
            }

          
            _context.PasswordResetOtps.Remove(storedOtp);
            await _context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(message: "OTP verified successfully.");
        }

       

        public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
         

            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (admin == null)
            {
                return ApiResponse<bool>.FailureResponse("Admin not found."); 
            }

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return ApiResponse<bool>.FailureResponse("New password and confirm password do not match.");
            }
            if (request.NewPassword.Length < 8 ||
                !request.NewPassword.Any(char.IsUpper) ||
                !request.NewPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return ApiResponse<bool>.FailureResponse("Password does not meet complexity requirements: at least 8 characters, one uppercase, one special character.");
            }


            admin.Password = request.NewPassword;
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(message: "Password reset successfully.");
        }

        private string GenerateOtp()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                int randomNumber = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
                return (randomNumber % 900000 + 100000).ToString();
            }
        }

        
        #endregion

        #region Logout
        public async Task RevokeTokenAsync(string jti, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(jti))
            {
                _logger.LogWarning("Attempted to revoke a token with null or empty JTI.");
                return;
            }

            if (await _context.RevokedTokens.AnyAsync(rt => rt.Jti == jti))
            {
                _logger.LogInformation("Token with JTI '{Jti}' is already revoked. No action taken.", jti);
                return;
            }

            var revokedToken = new RevokedToken
            {
                Jti = jti,
                ExpirationDate = expirationDate
            };

            await _context.RevokedTokens.AddAsync(revokedToken);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Token with JTI '{Jti}' successfully revoked until {ExpirationDate}.", jti, expirationDate);
        }

        public async Task<bool> IsTokenRevokedAsync(string jti)
        {
            if (string.IsNullOrWhiteSpace(jti))
            {
                return false;
            }
            return await _context.RevokedTokens.AnyAsync(rt => rt.Jti == jti && rt.ExpirationDate > DateTime.UtcNow);
        }

        public async Task PruneExpiredTokensAsync()
        {
            var cutoffDate = DateTime.UtcNow;
            var tokensToRemove = await _context.RevokedTokens
                                             .Where(rt => rt.ExpirationDate <= cutoffDate)
                                             .ToListAsync();
            if (tokensToRemove.Any())
            {
                _context.RevokedTokens.RemoveRange(tokensToRemove);
                await _context.SaveChangesAsync();
                _logger.LogInformation("{Count} expired revoked token entries pruned from the denylist.", tokensToRemove.Count);
            }
            else
            {
                _logger.LogInformation("No expired revoked tokens to prune.");
            }
        }
        #endregion
    }
}
