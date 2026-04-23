namespace Traverse.Repository.Graph
{
    public interface IEdgeRepository<T>
    {
        Task SaveEdgesAsync(IEnumerable<T> edges);
        Task<IEnumerable<T>> GetEdgesAsync(long id);
    }
}