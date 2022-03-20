using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cacheable.Kernel
{
    public class CacheableOptions
    {
        public static CacheableOptions Current;

        public CacheableOptions()
        {
            Current = this;
        }

        public string Host { get; set; }
        public int SlidingExpiration { get; set; }
    }
}
