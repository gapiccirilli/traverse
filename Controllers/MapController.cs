using Microsoft.AspNetCore.Mvc;
using Traverse.Models.Dto;
using Traverse.Models.Records;
using Traverse.Services;
using Traverse.Services.Maps;
using Traverse.Utility.Classes;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController(IMapService mapService) : ControllerBase
    {
        private readonly IMapService _mapService = mapService;

        [HttpGet("etas")]
        public async Task<IActionResult> GetEtasAsync([FromQuery] string origin, [FromQuery] string destination)
        {
            Coordinate originCoord = CoordUtility.ParseCoordinateString(origin);
            Coordinate destinationCoord = CoordUtility.ParseCoordinateString(destination);

            return Ok(await _mapService.GetEtasAsync(originCoord, destinationCoord));
        }

        [HttpGet("places")]
        public async Task<IActionResult> GetPlacesAsync([FromQuery] string query)
        {
            return Ok(await _mapService.GetPlacesAsync(query));
        }

        [HttpGet("routes")]
        public async Task<IActionResult> GetRoutesAsync([FromQuery] string origin, [FromQuery] string destination)
        {
            Coordinate originCoord = CoordUtility.ParseCoordinateString(origin);
            Coordinate destinationCoord = CoordUtility.ParseCoordinateString(destination);

            return Ok(await _mapService.GetRoutesAsync(originCoord, destinationCoord));
        }
    }
}