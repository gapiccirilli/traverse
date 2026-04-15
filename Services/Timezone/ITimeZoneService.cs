namespace Traverse.Services.Timezone
{
    public interface ITimeZoneService
    {
        Task<string> GetTimeZoneAsync(double latitude, double longitude);
    }
}