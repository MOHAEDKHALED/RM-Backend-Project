using Microsoft.AspNetCore.Identity;

namespace RadwaMintaWebAPI.Models.Entities
{
    public class AdminUser : BaseEntity<int>
    {
        public string Email { get; set; } = default!;
       public string Password { get; set; } = default!;
    }
}
