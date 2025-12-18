using RadwaMintaWebAPI.DTOs.ContactUs;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IContactService
    {
        Task<bool> SendContactMessageAsync(ContactMessageDto messageDto);
    }
}
