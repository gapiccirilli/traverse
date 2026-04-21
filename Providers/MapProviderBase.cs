using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;
using Traverse.Services.Maps;

namespace Traverse.Providers
{
    public abstract class MapProviderBase<A> : IMapProvider<EventDto, Transportation>
    {
        protected readonly HttpClient _httpClient;
        protected readonly IMapTokenService<A> _tokenService;

        protected MapProviderBase(HttpClient httpClient, IMapTokenService<A> tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }
        
        public abstract Task<GeocodeResult> GeocodeAsync(string address);

        public abstract Task<IEnumerable<Transportation>> GetEtasAsync(EventDto origin, IEnumerable<EventDto> nodes);

        public abstract Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
    }
}