namespace RadwaMintaWebAPI.Models.Entities
{
    public class PasswordResetOtp : BaseEntity<int>
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    }
}
