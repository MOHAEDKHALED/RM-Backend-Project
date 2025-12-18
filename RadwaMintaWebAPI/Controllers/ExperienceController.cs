using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("YearsOfExperience")] 
        public async Task<ActionResult<ExperienceCounterDto>> GetExperienceYears()
        {
            var experienceCounter = await _serviceManager.ExperienceService.GetYearsOfExperienceAsync();
            return Ok(experienceCounter);
        }
    }
}
