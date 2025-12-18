using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RadwaMintaWebAPI.DTOs.Authentication;
using RadwaMintaWebAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService, ILogger<AuthController> _logger, IConfiguration _configuration) : ControllerBase
    {
        #region Login
        [HttpPost("login")]


        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        #endregion

        #region Forgot Password

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault()));
            }
            var response = await _authService.ForgetPasswordRequestAsync(request.Email);
            return Ok(response);
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ForgotPasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault()));
            }
            var response = await _authService.ResendOtpAsync(request.Email);
            return Ok(response);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault()));
            }
            var response = await _authService.VerifyOtpAsync(request.Email, request.Otp);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault()));
            }
            var response = await _authService.ResetPasswordAsync(request);
            return Ok(response);
        }

        #endregion

        #region Logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var jtiClaim = User.FindFirst(JwtRegisteredClaimNames.Jti);
                if (jtiClaim == null || string.IsNullOrWhiteSpace(jtiClaim.Value))
                {
                    _logger.LogWarning("Logout attempt: Token missing JTI claim. User: {UserIdentifier}", User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Unknown");
                   
                    return Ok(ApiResponse<object>.SuccessResponse(null, "Logout Processed Successfuly."));
                }

                var jti = jtiClaim.Value;
                DateTime tokenExpirationDate;

                var expClaim = User.FindFirst("exp");
                if (expClaim != null && long.TryParse(expClaim.Value, out long expUnixTimestamp))
                {
                    tokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(expUnixTimestamp).UtcDateTime;
                    _logger.LogInformation("Token JTI '{Jti}' for user '{UserId}' will be revoked. Original expiration from 'exp' claim: {ExpirationDate}", jti, User.FindFirstValue(ClaimTypes.NameIdentifier), tokenExpirationDate);
                }
                else
                {
                    var jwtSettings = _configuration.GetSection("Jwt");
                    double defaultExpirationMinutes = 60; 
                    if (double.TryParse(jwtSettings["AccessTokenExpirationMinutes"], out double configuredMinutes) && configuredMinutes > 0)
                    {
                        defaultExpirationMinutes = configuredMinutes;
                    }
                    tokenExpirationDate = DateTime.UtcNow.AddMinutes(defaultExpirationMinutes);
                    _logger.LogWarning("Token JTI '{Jti}' for user '{UserId}': 'exp' claim not found or invalid. Using calculated expiration: {CalculatedExpirationDate}. Pruning is important.", jti, User.FindFirstValue(ClaimTypes.NameIdentifier), tokenExpirationDate);
                }

                await _authService.RevokeTokenAsync(jti, tokenExpirationDate);

                return Ok(ApiResponse<object>.SuccessResponse(null, "Successfully logged out. Token has been scheduled for revocation."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the logout process for user {UserId}.", User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Unknown");
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.FailureResponse("An error occurred on the server during logout. Please ensure you clear the token."));
            }
        }
        #endregion
    }
}
