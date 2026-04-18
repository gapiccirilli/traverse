using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Models.Records.Maps;
using Traverse.Services;
using Traverse.Services.Maps;

namespace Traverse.Controllers.Actions
{
    [ApiController]
    [Route("api")]
    public class OptimizeController(IOptimizationService<long, ItineraryGraph> optimizationService) : ControllerBase
    {
        private readonly IOptimizationService<long, ItineraryGraph> _routeOptimizationService = optimizationService;

        [HttpPost("itineraries/{itineraryId}/events/optimize")]
        public async Task<IActionResult> OptimizeRoutes(long itineraryId)
        {
            var etaResults = await _routeOptimizationService.Optimize(itineraryId);
            return Ok(etaResults);
        }
    }
}