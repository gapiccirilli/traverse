using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;
using Traverse.Options;
using Traverse.Services.Maps;

namespace Traverse.Providers.Impl
{
    public class AppleMapsProvider : MapProviderBase<string>
    {
        private readonly MapsOptions _options;
        private readonly ILogger _logger;
        public AppleMapsProvider(HttpClient httpClient, IOptions<MapsOptions> options, IMapTokenService<string> tokenService, ILogger<AppleMapsProvider> logger) : base(httpClient, tokenService)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUri);
            _logger = logger;
        }

        public async override Task<GeocodeResult> GeocodeAsync(string address)
        {
            throw new NotImplementedException();
        }

        public async override Task<Dictionary<long, IEnumerable<EtaWrapper>>> GetEtasAsync(IEnumerable<EventDto> nodes)
        {
            Dictionary<long, IEnumerable<EtaWrapper>> etas = [];
            Dictionary<long, IEnumerable<EventDto>> nodeMap = [];
            Dictionary<long, List<Task<EtaWrapper>>> tasks = [];

            nodeMap = nodes.ToDictionary(node => node.Id, node => nodes.Where(n => node.Id != n.Id));

            Dictionary<string, string?> queryParams = [];
            queryParams.Add("origin", string.Empty);
            queryParams.Add("destination", string.Empty);


            foreach (EventDto origin in nodes)
            {
                var destinations = nodeMap[origin.Id];

                string originQuery = $"{origin.Coordinates.Latitude},{origin.Coordinates.Longitude}";

                tasks.Add(origin.Id, []);

                foreach (EventDto destination in destinations)
                {
                    string destinationsQuery = $"{destination.Coordinates.Latitude},{destination.Coordinates.Longitude}";
                    string fullUri = $"{_options.EtaApi}?origin={originQuery}&destinations={destinationsQuery}";

                    tasks[origin.Id].Add(GetHttpResponseAsync<EtaWrapper>(fullUri).ContinueWith(task =>
                    {
                        task.Result.EventId = destination.Id;
                        return task.Result;
                    }));
                }
            }
            
            await Task.WhenAll(tasks.Values.SelectMany(t => t));

            foreach(KeyValuePair<long, List<Task<EtaWrapper>>> entry in tasks)
            {
                IEnumerable<EtaWrapper> results = entry.Value.Select(resultTask => resultTask.Result);
                etas.Add(entry.Key, results);
            }

            return etas;
        }

        public async override Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination)
        {
            throw new NotImplementedException();
        }

        private async Task<T> GetHttpResponseAsync<T>(string uri)
        {

            var token = await _tokenService.GetTokenAsync(new Uri($"{_options.BaseUri}{_options.AuthApi}"), _options.RefreshToken);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException($"Failed to deserialize Apple Maps {uri} response. Response was null.");;
        }
    }
}