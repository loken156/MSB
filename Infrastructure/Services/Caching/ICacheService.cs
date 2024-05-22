namespace Infrastructure.Services.Caching
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? timeSpan = null);
        Task RemoveAsync(string key);
    }


}
