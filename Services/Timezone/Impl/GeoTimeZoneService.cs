using GeoTimeZone;

namespace Traverse.Services.Timezone.Impl
{
    internal class GeoTimeZoneService : ITimeZoneService
    {
        public async Task<string> GetTimeZoneAsync(double latitude, double longitude)
        {
            return TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
        }
    }
}