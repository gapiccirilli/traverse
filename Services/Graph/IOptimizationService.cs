namespace Traverse.Services.Maps
{
    public interface IOptimizationService<T, R>
    {
        Task<R> Optimize(T optParam);
    }
}