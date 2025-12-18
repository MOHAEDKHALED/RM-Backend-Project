using RadwaMintaWebAPI.DTOs.Quality;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IQualityService
    {
        Task<IEnumerable<QualityDTo>> GetAllQualitiesAsync(string lang);
    }
}
