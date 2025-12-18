using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Quality;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityController(IServiceManager _serviceManager) : ControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<QualityDTo>>> GetAllQualities()
        //{
        //    var Qualities = await _serviceManager.QualityService.GetAllQualitiesAsync();
        //    return Ok(Qualities);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QualityDTo>>> GetAllQualities(string lang)
        {
            var qualities = await _serviceManager.QualityService.GetAllQualitiesAsync(lang);
            return Ok(qualities);
        }
    }
}
