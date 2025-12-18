using RadwaMintaWebAPI.DTOs.Media;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IMediaService
    {
        Task<SocialMediaDTo> GetSocialMediaLinksAsync();
        Task<WhatsAppDTo> GetWhatsAppLinkAsync();
        Task<PlatformDTo> GetPlatformLinkAsync();



    }
}
