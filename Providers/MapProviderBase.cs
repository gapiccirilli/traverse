using Traverse.Models;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;
using Traverse.Services.Maps;

namespace Traverse.Providers
{
    public abstract class MapProviderBase<A> : IMapProvider<Event>
    {
        protected readonly HttpClient _httpClient;
        protected readonly IMapTokenService<A> _tokenService;

        protected MapProviderBase(HttpClient httpClient, IMapTokenService<A> tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }
        
        public abstract Task<GeocodeResult> GeocodeAsync(string address);

        public abstract Task<Dictionary<long, IEnumerable<EtaResult>>> GetEtasAsync(IEnumerable<Event> nodes);

        public abstract Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
    }
}