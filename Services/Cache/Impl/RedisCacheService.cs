

using Newtonsoft.Json;
using StackExchange.Redis;

namespace Traverse.Services.Cache.Impl
{
    public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
    {
        private readonly IDatabase _db = redis.GetDatabase();
        private const int DEFAULT_EXPIRY_HOURS = 0;
        private const int DEFAULT_EXPIRY_MINUTES = 10;
        private const int DEFAULT_EXPIRY_SECONDS = 0;

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            
            if (value.IsNullOrEmpty) return default;

            return JsonConvert.DeserializeObject<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonConvert.SerializeObject(value);

            await _db.StringSetAsync(key, json, expiry ?? new TimeSpan(DEFAULT_EXPIRY_HOURS, DEFAULT_EXPIRY_MINUTES, DEFAULT_EXPIRY_SECONDS));
        }
    }
}