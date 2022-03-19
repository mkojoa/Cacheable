using System;
using System.Threading.Tasks;

namespace StackExchange.RedisCache.Cacheable.Services
{
    public interface ICacheable
    {
        Task<TResult> RememberAsync<TResult>(string key, Func<Task<TResult>> cacheFunc);

        Task<TResult> GetAsync<TResult>(string key);

        //Task<bool> HasAsync(string key);
    }
}
