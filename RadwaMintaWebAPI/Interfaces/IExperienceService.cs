using RadwaMintaWebAPI.DTOs;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IExperienceService
    {
        Task<ExperienceCounterDto> GetYearsOfExperienceAsync();
    }
}
