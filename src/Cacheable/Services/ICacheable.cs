using System;
using System.Threading.Tasks;

namespace Cacheable.Services
{
    public interface ICacheable
    {
        Task<TResult> RememberAsync<TResult>(string key, Func<Task<TResult>> cacheFunc, TimeSpan expiresIn);

        Task<TResult> GetAsync<TResult>(string key);
    }
}
