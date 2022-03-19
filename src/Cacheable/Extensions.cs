using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.RedisCache.Cacheable.Kernel;
using StackExchange.RedisCache.Cacheable.Services;

namespace StackExchange.RedisCache.Cacheable
{
    public static class Extensions
    {
        public static IServiceCollection AddCacheable(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var appsettingsData = new CacheableOptions();
            configuration.Bind("CacheableOptions", appsettingsData);
            services.AddSingleton(appsettingsData);

            services.AddSingleton(configuration);

            services.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = CacheableOptions.Current.Host;
            });

            services.AddScoped<ICacheable, CacheableService>();
            return services;
        }

        public static IApplicationBuilder UseCacheable(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
