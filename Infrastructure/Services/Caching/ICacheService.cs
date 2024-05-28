namespace Infrastructure.Services.Caching
{
    public interface ICacheService
    {
        // This interface is used to cache data in memory, here can you determine how long the data should be cached
        // just adjust the timespan.
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? timeSpan = null);
        Task RemoveAsync(string key);
    }


}
