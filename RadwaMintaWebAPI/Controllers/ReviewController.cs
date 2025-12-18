using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Reviews;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet("GetReview")]
        public async Task<IActionResult> GetAll([FromQuery] string lang = "en")
        {
            var reviews = await _serviceManager.ReviewService.GetAllReviewsAsync(lang);
            return Ok(reviews);
        }

        [HttpGet("GetReviewById{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _serviceManager.ReviewService.GetReviewByIdAsync(id);
            return Ok(review);
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newReview = await _serviceManager.ReviewService.CreateReviewAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newReview.Id }, newReview);
        }

        [HttpDelete("DeleteReviewById{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.ReviewService.DeleteReviewAsync(id);
            return NoContent();
        }
        //[HttpGet("GetReview")]
        //public async Task<ActionResult<IEnumerable<ReviewResponseDTo>>> GetAllReviews(string lang)
        //{
        //    var reviews = await _serviceManager.ReviewService.GetAllReviewsAsync(lang);
        //    return Ok(reviews);
        //}

        //[HttpPost("AddReview")]
        //public async Task<ActionResult<ReviewResponseDTo>> CreateReview([FromBody] ReviewRequestDTo reviewRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var createdReview = await _serviceManager.ReviewService.CreateReviewAsync(reviewRequest);
        //    return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
        //}

        //[HttpGet("GetReviewById{id}")]
        //public async Task<ActionResult<ReviewResponseDTo>> GetReviewById(int id)
        //{
        //    try
        //    {
        //        var review = await _serviceManager.ReviewService.GetReviewByIdAsync(id);
        //        return Ok(review);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpDelete("DeleteReviewById{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteReview(int id)
        //{
        //    try
        //    {
        //        await _serviceManager.ReviewService.DeleteReviewAsync(id);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
    }
}
