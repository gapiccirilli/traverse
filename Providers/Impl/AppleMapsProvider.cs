using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Traverse.Models;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;
using Traverse.Options;
using Traverse.Services.Maps;

namespace Traverse.Providers.Impl
{
    public class AppleMapsProvider : MapProviderBase<string>
    {
        private readonly MapsOptions _options;
        public AppleMapsProvider(HttpClient httpClient, IOptions<MapsOptions> options, IMapTokenService<string> tokenService) : base(httpClient, tokenService)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUri);
        }

        public async override Task<GeocodeResult> GeocodeAsync(string address)
        {
            throw new NotImplementedException();
        }

        public async override Task<Dictionary<long, IEnumerable<EtaResult>>> GetEtasAsync(IEnumerable<Event> nodes)
        {
            Dictionary<long, IEnumerable<EtaResult>> etas = [];
            Dictionary<long, IEnumerable<Event>> nodeMap = [];
            Dictionary<long, Task<EtaWrapper>> tasks = [];

            nodeMap = nodes.ToDictionary(node => node.Id, node => nodes.Where(n => node.Id != n.Id));

            foreach(Event eventNode in nodes)
            {
                string destinationStr = string.Join('|', nodeMap[eventNode.Id].Select(n => $"{n.Coordinates.X},{n.Coordinates.Y}" ));
                string fullUri = $"{_options.EtaApi}?origin={eventNode.Coordinates.X},{eventNode.Coordinates.Y}&destinations={destinationStr}";
                
                tasks.Add(eventNode.Id, GetHttpResponseAsync<EtaWrapper>(fullUri));
            }

            await Task.WhenAll(tasks.Values);

            foreach(KeyValuePair<long, Task<EtaWrapper>> entry in tasks)
            {
                etas.Add(entry.Key, entry.Value.Result.Etas);
            }

            return etas;
        }

        public async override Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination)
        {
            throw new NotImplementedException();
        }

        private async Task<T> GetHttpResponseAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException($"Failed to deserialize Apple Maps {uri} response. Response was null.");;
        }
    }
}