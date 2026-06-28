using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;
using Traverse.Providers;

namespace Traverse.Services.Maps.Impl
{
    public class AppleMapsService(IMapProvider<EventDto, Transportation> mapProvider) : IMapService
    {
        private readonly IMapProvider<EventDto, Transportation> _appleMapsProvider = mapProvider;

        public async Task<EtaResult> GetEtasAsync(Coordinate origin, Coordinate destination)
        {
            return await _appleMapsProvider.GetEtasAsync(origin, destination);
        }

        public async Task<GeocodeResult> GetPlacesAsync(string query)
        {
            return await _appleMapsProvider.GeocodeAsync(query);
        }

        public async Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination)
        {
            return await _appleMapsProvider.GetRoutesAsync(origin, destination);
        }
    }
}