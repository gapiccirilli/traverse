using Traverse.Models.Records;
using Traverse.Models.Records.Maps;

namespace Traverse.Providers
{
    public interface IMapProvider<T>
    {
        Task<Dictionary<long, IEnumerable<EtaWrapper>>> GetEtasAsync(IEnumerable<T> nodes);
        Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
        Task<GeocodeResult> GeocodeAsync(string address);
    }
}