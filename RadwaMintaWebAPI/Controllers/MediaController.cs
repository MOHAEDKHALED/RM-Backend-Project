using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Media;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("MediaLinks")] 
        public async Task<ActionResult<SocialMediaDTo>> GetSocialMediaLinks()
        {
            var links = await _serviceManager.MediaService.GetSocialMediaLinksAsync();
            if (links == null)
            {
                return NotFound("Social media links not found.");
            }
            return Ok(links);
        }

        [HttpGet("WhatsAppLink")]
        public async Task<ActionResult<WhatsAppDTo>> GetWhatsAppLink()
        {
            var link = await _serviceManager.MediaService.GetWhatsAppLinkAsync();
            if (link == null || string.IsNullOrEmpty(link.WhatsApp))
            {
                return NotFound("WhatsApp link not found.");
            }
            return Ok(link);
        }

        [HttpGet("PlatformLink")]
        public async Task<ActionResult<PlatformDTo>> GetPlatformLink()
        {
            var link = await _serviceManager.MediaService.GetPlatformLinkAsync();
            if (link == null || string.IsNullOrEmpty(link.Platform))
            {
                return NotFound("Platform link not found.");
            }
            return Ok(link);
        }

        // For Admin 

        //[HttpPut("UpdateMediaLinks")]
        //[Authorize]
        //public async Task<IActionResult> UpdateSocialLinks([FromBody] SocialMediaDTo model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }



        //    var success = await _serviceManager.MediaService.UpdateSocialLinksAsync(model);

        //    if (success)
        //    {
        //        return Ok(new { message = "Social links updated successfully." });
        //    }
        //    else
        //    {
        //        return StatusCode(500, new { message = "Failed to update social links." });
        //    }
        //}

    }
}