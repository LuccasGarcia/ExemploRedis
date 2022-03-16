using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExemploRedis.Extensions
{
    public static class DistributedCacheExtension
    {
        public static IServiceCollection AddDistributedCache(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = configuration["Redis:InstanceName"];
            });
            return services;
        }
    }
}
