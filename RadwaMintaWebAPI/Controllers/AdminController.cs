using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Media;

namespace RadwaMintaWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("MediaLinks")]
        public async Task<ActionResult<SocialMediaDTo>> GetSocialMediaLinks()
        {
            var links = await _serviceManager.AdminService.GetSocialMediaLinksAsync();
            if (links == null)
            {
                return NotFound("Social media links not found.");
            }
            return Ok(links);
        }

        [HttpPut("UpdateMediaLinks")]
        public async Task<IActionResult> UpdateSocialMediaLinks([FromBody] SocialMediaDTo socialLinksDto)
        {
            if (socialLinksDto == null)
                return BadRequest("Invalid data.");

            var isUpdated = await _serviceManager.AdminService.UpdateSocialLinksAsync(socialLinksDto);

            if (!isUpdated)
                return NotFound("Social media links not found.");

            return Ok("Social media links updated successfully.");
        }
    }
}
