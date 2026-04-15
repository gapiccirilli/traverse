using Microsoft.AspNetCore.Mvc;
using Traverse.Models.Dto;
using Traverse.Services;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController(ITripService tripService) : ControllerBase
    {
        private readonly ITripService _tripService = tripService;

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await _tripService.GetAllTripsAsync());
        }

        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetTripById(long tripId)
        {
            return Ok(await _tripService.GetTripByIdAsync(tripId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] TripDto trip)
        {
            return CreatedAtAction(nameof(GetTripById), new { tripId = trip.Id }, await _tripService.CreateTripAsync(trip));
        }

        [HttpPut("{tripId}")]
        public async Task<IActionResult> UpdateTrip(long tripId, [FromBody] TripDto trip)
        {
            return Ok(await _tripService.UpdateTripAsync(tripId, trip));
        }

        [HttpDelete("{tripId}")]
        public async Task<IActionResult> DeleteTrip(long tripId)
        {
            await _tripService.DeleteTripAsync(tripId);
            return NoContent();
        }
    }
}
