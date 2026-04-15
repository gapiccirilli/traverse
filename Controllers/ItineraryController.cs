using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Services;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/trips/{tripId}/itineraries")]
    public class ItineraryController(IItineraryService itineraryService) : ControllerBase
    {
        private readonly IItineraryService _itineraryService = itineraryService;

        [HttpGet]
        public async Task<IActionResult> GetItineraries(long tripId)
        {
            return Ok(await _itineraryService.GetItinerariesAsync(tripId));
        }

        [HttpGet("{itineraryId}")]
        public async Task<IActionResult> GetItineraryById(long tripId, long itineraryId)
        {
            return Ok(await _itineraryService.GetItineraryByIdAsync(tripId, itineraryId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateItinerary(long tripId, [FromBody] ItineraryDto itinerary)
        {
            return CreatedAtAction(nameof(GetItineraryById), new { tripId, itineraryId = itinerary.Id }, await _itineraryService.CreateItineraryAsync(tripId, itinerary));
        }

        [HttpPut("{itineraryId}")]
        public async Task<IActionResult> UpdateItinerary(long tripId, long itineraryId, [FromBody] ItineraryDto itinerary)
        {
            return Ok(await _itineraryService.UpdateItineraryAsync(tripId, itineraryId, itinerary));
        }

        [HttpDelete("{itineraryId}")]
        public async Task<IActionResult> DeleteItinerary(long tripId, long itineraryId)
        {
            await _itineraryService.DeleteItineraryAsync(tripId, itineraryId);
            return NoContent();
        }
    }
}