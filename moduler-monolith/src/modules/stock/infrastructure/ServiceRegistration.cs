using core_application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stockInfrastructure.Persistence;

namespace stockInfrastructure
{
    public static class ServiceRegistration
    {
        public static void AddStockInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<StockDbContext>(options => options.UseNpgsql(config.GetConnectionString("ConnStr")));

            services.AddScoped<IDbContextHandler, StockDbContext>();

            var stockDbContext = services.BuildServiceProvider().GetRequiredService<StockDbContext>();
            stockDbContext.SeedData();
        }
    }
}
