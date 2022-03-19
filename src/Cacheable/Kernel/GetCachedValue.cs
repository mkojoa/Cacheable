using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.RedisCache.Cacheable.Kernel
{
    public record GetCachedValue<T>(bool Cached, T value);
}
