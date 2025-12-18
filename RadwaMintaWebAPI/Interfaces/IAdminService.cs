using RadwaMintaWebAPI.DTOs.Media;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IAdminService
    {
        Task<SocialMediaDTo> GetSocialMediaLinksAsync();
        Task<bool> UpdateSocialLinksAsync(SocialMediaDTo socialLinksDto);
    }
}
