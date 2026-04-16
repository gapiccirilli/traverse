namespace Traverse.Services.Maps
{
    public interface IOptimizationService<T, R>
    {
        Task<IEnumerable<R>> Optimize(List<T> values);
    }
}