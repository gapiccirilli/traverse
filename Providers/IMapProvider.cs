using Traverse.Models.Graph;
using Traverse.Models.Records;
using Traverse.Models.Records.Maps;

namespace Traverse.Providers
{
    public interface IMapProvider<T, R>
    {
        Task<R> GetEtasAsync(T currentNode, T previousNode);
        Task<RouteResult> GetRoutesAsync(Coordinate origin, Coordinate destination);
        Task<GeocodeResult> GeocodeAsync(string address);
        Task<IEnumerable<R>> OptimizeAsync(T origin, IEnumerable<T> nodes);
    }
}