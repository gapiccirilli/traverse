using Traverse.Models.Records;
using Traverse.Models.Records.Maps;

namespace Traverse.Providers
{
    public interface IMapProvider<T, R>
    {
        Task<IEnumerable<R>> GetEtasAsync(T origin, IEnumerable<T> nodes);
        Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
        Task<GeocodeResult> GeocodeAsync(string address);
    }
}