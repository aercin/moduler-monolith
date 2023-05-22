using Microsoft.Extensions.DependencyInjection;
using stockApplication;
using stockDomain.Abstractions;
using System.Reflection;

namespace stockApplication
{
    public static class ServiceRegistration
    {
        public static void AddStockApplication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IStockDomainService, StockDomainService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
