namespace Traverse.Services.Graph
{
    public interface IEdgeService<T, R>
    {
        Task BuildEdgesAsync(IEnumerable<T> nodes);
        Task BuildEdgesAsync(T node, long id);
    }
}