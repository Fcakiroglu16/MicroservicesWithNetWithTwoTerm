using Microsoft.Extensions.Caching.Memory;
using Order.Application;

namespace Caching
{
    public class CacheService(IMemoryCache cache) : ICacheService
    {
        public void Add<T>(string key, T value, TimeSpan expirationTime)
        {
            cache.Set(key, value, expirationTime);
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }
    }
}