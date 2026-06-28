using Traverse.Models.Records;
using Traverse.Models.Records.Maps;

namespace Traverse.Services.Maps
{
    public interface IMapService
    {
        Task<GeocodeResult> GetPlacesAsync(string query);
        Task<EtaResult> GetEtasAsync(Coordinate origin, Coordinate destination);
        Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
    }
}