using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using paymentApplication;
using paymentInfrastructure;

namespace paymentApi
{
    public static class ServiceRegistration
    {
        public static void AddPaymentModule(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddPaymentApplication();
            services.AddPaymentInfrastructure(config);
        }
    }
}
