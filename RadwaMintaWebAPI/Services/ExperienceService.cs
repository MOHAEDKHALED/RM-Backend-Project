using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Services
{
    public class ExperienceService(IUnitOfWork _unitOfWork) : IExperienceService
    {
        public async Task<ExperienceCounterDto> GetYearsOfExperienceAsync()
        {
           
            var settings = await _unitOfWork.GetRepository<ExperienceSettings, int>().GetByIdAsync(1); //Id=1 مثلاً

            if (settings == null)
            {
            
                return new ExperienceCounterDto { YearsOfExperience = 0 };
            }

            DateTime startDate = settings.StartDate;
            DateTime today = DateTime.UtcNow; 

            int years = today.Year - startDate.Year;

            if (startDate.Date > today.AddYears(-years).Date)
            {
                years--;
            }

            return new ExperienceCounterDto { YearsOfExperience = years };
        }
    }
}
