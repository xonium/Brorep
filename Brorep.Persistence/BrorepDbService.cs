using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Brorep.Persistence
{
    public static class BrorepDbService
    {
        public static IServiceCollection AddDbDependencies(this IServiceCollection services, string connectionString)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BrorepDbContext>((serviceProvider, options) =>
                    options.UseSqlServer(connectionString).UseInternalServiceProvider(serviceProvider)
                );

            return services;
        }

    }
}
