using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Services;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/events/{eventId}/[controller]")]
    public class RatingsController(IRatingService ratingService) : ControllerBase
    {
        private readonly IRatingService _ratingService = ratingService;

        [HttpGet]
        public async Task<IActionResult> GetRatings(long eventId)
        {
            return Ok(await _ratingService.GetRatingByIdAsync(eventId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(long eventId, [FromBody] Rating rating)
        {
            var entity = await _ratingService.CreateRatingAsync(eventId, rating);
            return CreatedAtAction(nameof(GetRatings), new { eventId }, entity);
        }

        [HttpPut("{ratingId}")]
        public async Task<IActionResult> UpdateRating(long eventId, long ratingId, [FromBody] Rating rating)
        {
            return Ok(await _ratingService.UpdateRatingAsync(eventId, ratingId, rating));
        }
    }
}
