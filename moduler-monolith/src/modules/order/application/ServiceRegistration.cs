using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace orderApplication
{
    public static class ServiceRegistration
    {
        public static void AddOrderApplication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
