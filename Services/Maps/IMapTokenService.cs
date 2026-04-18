namespace Traverse.Services.Maps
{
    public interface IMapTokenService<T>
    {
        Task<T> GetTokenAsync(Uri uri, string refreshToken);
    }
}