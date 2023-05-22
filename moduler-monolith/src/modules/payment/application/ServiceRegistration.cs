using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace paymentApplication
{
    public static class ServiceRegistration
    {
        public static void AddPaymentApplication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
