using RadwaMintaWebAPI.DTOs.Authentication;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IAuthService
    {
        #region Login
        Task<ApiResponse<string>> LoginAsync(LoginRequestDTO loginRequestDTO);
        #endregion

        #region Forget Password
        Task<ApiResponse<bool>> ForgetPasswordRequestAsync(string email);
        Task<ApiResponse<bool>> ResendOtpAsync(string email);
        Task<ApiResponse<bool>> VerifyOtpAsync(string email, string otp);
        Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequestDTO request);
        #endregion

        #region Log Out
        Task RevokeTokenAsync(string jti, DateTime expirationDate);
        Task<bool> IsTokenRevokedAsync(string jti);
        Task PruneExpiredTokensAsync(); 
        #endregion

    }
}
