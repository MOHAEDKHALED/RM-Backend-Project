using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AdminUser admin);
    }
}
