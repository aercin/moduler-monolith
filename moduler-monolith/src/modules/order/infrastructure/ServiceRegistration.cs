using core_application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using orderInfrastructure.Persistence;

namespace orderInfrastructure
{
    public static class ServiceRegistration
    {
        public static void AddOrderInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(config.GetConnectionString("ConnStr")));
             
            services.AddScoped<IDbContextHandler, OrderDbContext>();    
        }
    }
}
