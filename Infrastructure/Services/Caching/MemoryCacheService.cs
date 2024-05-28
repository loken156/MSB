using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services.Caching
{

    /// <summary>
    /// This service is used to cache data in memory, the logic is to cache the creation info
    /// of a new order so that the orderid is created and then signed to the newly created box.
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public Task<T> GetAsync<T>(string key)
        {
            _cache.TryGetValue(key, out T cacheEntry);
            return Task.FromResult(cacheEntry);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? timeSpan = null)
        {
            var options = new MemoryCacheEntryOptions();

            if (timeSpan.HasValue)
            {
                options.SetAbsoluteExpiration(timeSpan.Value);
            }

            _cache.Set(key, value, options);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}