using Traverse.Models.Graph;

namespace Traverse.Services.Graph
{
    public interface IEdgeService<T, R>
    {
        Task BuildEdgesAsync(T node, long id);
        Task BuildEdgeAsync(T toNode, T? fromNode, long id);
        Task<IEnumerable<R>> GetEdges(long id, string cacheKey);
        Task<R> GetEdge(long id);
    }
}