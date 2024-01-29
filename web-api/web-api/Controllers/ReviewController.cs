using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_api.Models;
using web_api.Services;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = WebRoles.Member)]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("reviews/{id}")]
        [Authorize(Policy = WebRoles.Admin)]
        public async Task<ActionResult<List<Review>>> GetUserReviewsById(int Id)
        {
            List<Review>? Reviews = await _reviewService.GetUserReviewsFromId(Id);

            if (Reviews == null)
                return BadRequest("User Id not found");

            return Ok(Reviews);
        }

        [HttpGet("myreviews")]
        [Authorize(Policy = WebRoles.Member)]
        public async Task<ActionResult<List<Review>>> GetCurrentUserReviews()
        {
            string? username = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            if (username == null)
                return BadRequest("Username not found in jwt claim");

            List<Review>? Reviews = await _reviewService.GetUserReviewsFromUsername(username);

            if (Reviews == null)
                return BadRequest("Username not found");

            return Ok(Reviews);
        }

        //TODO: UPDATE, CREATE, DELETE
    }
}