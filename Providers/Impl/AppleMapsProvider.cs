using System.Collections;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
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

        public async override Task<IEnumerable<Transportation>> GetEtasAsync(EventDto origin, IEnumerable<EventDto> nodes)
        {

            if (!nodes.Any())
            {
                return [];
            }

            List<Task<EtaWrapper>> tasks = [];
            List<Transportation> edges = [];

            string originQuery = $"{origin.Coordinates.Latitude},{origin.Coordinates.Longitude}";

            foreach (EventDto eventNode in nodes)
            {

                string destinationsQuery = $"{eventNode.Coordinates.Latitude},{eventNode.Coordinates.Longitude}";
                string fullUri = $"{_options.EtaApi}?origin={originQuery}&destinations={destinationsQuery}";

                tasks.Add(GetHttpResponseAsync<EtaWrapper>(fullUri).ContinueWith(task =>
                {
                    task.Result.EventId = eventNode.Id;
                    return task.Result;
                }));
            }
            
            await Task.WhenAll(tasks);

            foreach(var task in tasks)
            {
                var etas = task.Result.Etas;

                if (!etas.Any())
                {
                    throw new InvalidOperationException($"An eta result was expected from {nameof(AppleMapsProvider)} between " +
                        $"origin {origin.Id} and destination {task.Result.EventId} but there were no results returned");
                }
                
                var etaResult = etas.First();

                edges.Add(new Transportation()
                {
                    FromEventId = origin.Id,
                    ToEventId = task.Result.EventId,
                    WeightSeconds = etaResult.ExpectedTravelTimeSeconds,
                    Distance = etaResult.DistanceMeters,
                    DistanceUnit = DistanceUnit.Meters
                });
            }

            return edges.AsEnumerable();
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