using System.Net.Http.Headers;
using System.Security.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Traverse.Models.Records.Maps;

namespace Traverse.Services.Maps.Impl
{
    public class AppleMapsTokenService : IMapTokenService<string>
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "AppleMapsAccessToken";

        public AppleMapsTokenService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        public async Task<string> GetTokenAsync(Uri uri, string refreshToken)
        {
            
            if (_cache.TryGetValue(CacheKey, out string token))
                return token!;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
            var auth = await FetchTokenFromAppleMapsServiceAsync(uri);
            var expiration = auth.ExpiresInSeconds - 30;
            token = auth.AccessToken;

            _cache.Set(CacheKey, token, TimeSpan.FromSeconds(expiration));
            return token;
        }

        private async Task<MapsAuthToken> FetchTokenFromAppleMapsServiceAsync(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri.AbsoluteUri);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new AuthenticationException($"Could not authenticate with Apple Maps Service. Status Code: {response.StatusCode}. Error: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MapsAuthToken>(json) ?? throw new InvalidOperationException("Failed to deserialize Apple Maps auth token. Response was null.");
        }
    }
}