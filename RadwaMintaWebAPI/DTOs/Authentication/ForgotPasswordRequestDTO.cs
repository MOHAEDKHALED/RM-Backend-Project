using System.ComponentModel.DataAnnotations;

namespace RadwaMintaWebAPI.DTOs.Authentication
{
    public class ForgotPasswordRequestDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
