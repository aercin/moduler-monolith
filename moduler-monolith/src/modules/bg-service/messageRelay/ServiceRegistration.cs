using messageRelayService;
using Microsoft.Extensions.DependencyInjection;

namespace messageRelay
{
    public static class ServiceRegistration
    {
        public static void AddMessageRelay(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHostedService<OrderWorker>();
            services.AddHostedService<StockWorker>();
            services.AddHostedService<PaymentWorker>();
        }
    }
}
