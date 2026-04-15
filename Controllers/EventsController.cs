using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Services;
using Traverse.Services.Timezone;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/itineraries/{itineraryId}/[controller]")]
    public class EventsController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpGet]
        public async Task<IActionResult> GetEvents(long itineraryId)
        {
            return Ok(await _eventService.GetAllEventsAsync(itineraryId));
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventById(long itineraryId, long eventId)
        {
            return Ok(await _eventService.GetEventByIdAsync(itineraryId, eventId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(long itineraryId, [FromBody] EventDto newEvent)
        {   
            return CreatedAtAction(nameof(GetEventById), new { itineraryId, eventId = newEvent.Id }, await _eventService.CreateEventAsync(itineraryId, newEvent));
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEvent(long itineraryId, long eventId, [FromBody] EventDto updatedEvent)
        {
            return Ok(await _eventService.UpdateEventAsync(itineraryId, eventId, updatedEvent));
        }

        [HttpPatch("{eventId}")]
        public async Task<IActionResult> PatchEvent(long itineraryId, long eventId, [FromBody] EventDto updatedEvent)
        {
            return Ok(await _eventService.PatchEventAsync(itineraryId, eventId, updatedEvent));
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(long itineraryId, long eventId)
        {
            await _eventService.DeleteEventAsync(itineraryId, eventId);
            return NoContent();
        }
    }
}