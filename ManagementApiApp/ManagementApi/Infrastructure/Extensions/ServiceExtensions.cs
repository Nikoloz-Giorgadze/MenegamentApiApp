using ManagementApi.Services.Infrastructure.Extensions;
using ManagementApi.Infrastructure.Infrastructure.Extensions;

namespace ManagementApi.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices();

            return services;
        }
    }
}
