using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
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

            Dictionary<string, string?> queryParams = [];
            queryParams.Add("origin", string.Empty);
            queryParams.Add("destination", string.Empty);


            foreach (Event origin in nodes)
            {
                var destinations = nodeMap[origin.Id];
                queryParams["origin"] = $"{origin.Coordinates.X},{origin.Coordinates.Y}";

                foreach (Event destination in destinations)
                {
                    queryParams["destination"] = $"{destination.Coordinates.X},{destination.Coordinates.Y}";
                    string fullUri = QueryHelpers.AddQueryString(_options.EtaApi, queryParams);

                    tasks.Add(origin.Id, GetHttpResponseAsync<EtaWrapper>(fullUri));
                }
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
            var token = await _tokenService.GetTokenAsync(new Uri(_options.AuthApi));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException($"Failed to deserialize Apple Maps {uri} response. Response was null.");;
        }
    }
}