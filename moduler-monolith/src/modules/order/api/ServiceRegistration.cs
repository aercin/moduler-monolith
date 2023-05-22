using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using orderApplication;
using orderInfrastructure;

namespace orderApi
{
    public static class ServiceRegistration
    {
        public static void AddOrderModule(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddOrderApplication();
            services.AddOrderInfrastructure(config);
        }
    }
}
