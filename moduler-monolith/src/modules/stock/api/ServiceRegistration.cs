using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stockApplication;
using stockInfrastructure;

namespace stockApi
{
    public static class ServiceRegistration
    {
        public static void AddStockModule(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddStockApplication();
            services.AddStockInfrastructure(config);
        }
    }
}
