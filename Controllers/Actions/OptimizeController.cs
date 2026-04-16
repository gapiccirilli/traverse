using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Models.Records.Maps;
using Traverse.Services;
using Traverse.Services.Maps;

namespace Traverse.Controllers.Actions
{
    [ApiController]
    [Route("api")]
    public class OptimizeController(IEventService eventService, IOptimizationService<Event, EtaResult> optimizationService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;
        private readonly IOptimizationService<Event, EtaResult> _routeOptimizationService = optimizationService;

        [HttpPost("itineraries/{itineraryId}/events/optimize")]
        public async Task<IActionResult> OptimizeRoutes(long itineraryId)
        {
            IEnumerable<EventDto> events = await _eventService.GetAllEventsAsync(itineraryId);

            return Ok();
        }
    }
}