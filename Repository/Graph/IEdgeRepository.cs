namespace Traverse.Repository.Graph
{
    public interface IEdgeRepository<T>
    {
        Task SaveEdgesAsync(IEnumerable<T> edges);
        Task SaveEdgeAsync(T edge);
        Task<IEnumerable<T>> GetEdgesAsync(long id);
    }
}