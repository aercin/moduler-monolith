using core_infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockDomain.Entities;

namespace stockInfrastructure.Persistence
{
    public class StockDbContext : BaseDbContext
    {
        private readonly IConfiguration _config;
        public StockDbContext(DbContextOptions<StockDbContext> options, IConfiguration config) : base(options)
        {
            this._config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("stock");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
        }

        public override string ContextId
        {
            get
            {
                return this._config.GetValue<string>("Modules:Stock:ContextId");
            }
        }

        public DbSet<Stock> Stocks { get; set; }
    }
}
