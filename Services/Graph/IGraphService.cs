using Traverse.Models;
using Traverse.Models.Graph;

namespace Traverse.Services.Graph
{
    public interface IGraphService<G, N, E>
    {
        Task<G> GetGraphAsync(long itineraryId);
        G BuildGraph(IEnumerable<N> nodes, IEnumerable<E> edges);
    }
}