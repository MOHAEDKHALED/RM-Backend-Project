using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.DTOs.ContactUs;
using RadwaMintaWebAPI.Interfaces;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IContactService _contactService) : ControllerBase
    {


        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ContactMessageDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _contactService.SendContactMessageAsync(messageDto);

            if (success)
            {
                return Ok(new { Message = "Your message has been sent successfully!" });
            }
            else
            {
                return StatusCode(500, new { Message = "There was an error sending your message. Please try again later." });
            }
        }
    }
}
