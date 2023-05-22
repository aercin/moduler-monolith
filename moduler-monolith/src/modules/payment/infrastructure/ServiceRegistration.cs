using core_application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using paymentInfrastructure.Persistence;

namespace paymentInfrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPaymentInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<PaymentDbContext>(options => options.UseNpgsql(config.GetConnectionString("ConnStr")));
             
            services.AddScoped<IDbContextHandler, PaymentDbContext>();    
        }
    }
}
