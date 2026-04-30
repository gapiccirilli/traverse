using Traverse.Models.Graph;

namespace Traverse.Services.Graph
{
    public interface IEdgeService<T, R>
    {
        Task BuildEdgesAsync(T node, long id);
        Task<IEnumerable<R>> GetEdges(long id);
    }
}