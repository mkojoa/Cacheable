using Cacheable.Kernel;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cacheable.Services
{
    public class CacheableService : ICacheable
    {
        private readonly IDistributedCache _driver;

        public CacheableService(IDistributedCache driver)
        {
            _driver = driver;
        }


        public async Task<TResult> RememberAsync<TResult>(string key, Func<Task<TResult>> cacheFunc)
        {
            return await TryCachingEntryAsync(key, cacheFunc);
        }


        public async Task<TResult> GetAsync<TResult>(string key)
        => await GetCacheItemAsync<TResult>(key);

        private async Task<T> TryCachingEntryAsync<T>
            (string key, Func<Task<T>> cacheFunc)
        {
            var cachedItem = await GetCacheItemAsync<T>(key);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            T result = await cacheFunc();

            await _driver.SetStringAsync(key, JsonConvert.SerializeObject(result), AbsoluteExpiration());

            return result;
        }

        private async Task<T> GetCacheItemAsync<T>(string key)
        {
            var jsonValue = await _driver.GetStringAsync(key);
            if (string.IsNullOrEmpty(jsonValue))
            {
                return JsonConvert.DeserializeObject<T>("");
            }

            return JsonConvert.DeserializeObject<T>(jsonValue);

        }

        private static DistributedCacheEntryOptions AbsoluteExpiration()
        {
            // === TimeSpanType
            //FromDays
            //FromHours
            //FromMilliseconds
            //FromMinutes
            //FromSeconds
            // === 

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow 
                = TimeSpan.FromSeconds(CacheableOptions.Current.ExpiresAt),

                SlidingExpiration = TimeSpan.FromMinutes(60)
            };

            return options;
        }
    }
}
