using core_infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using orderDomain.Entities;

namespace orderInfrastructure.Persistence
{
    public class OrderDbContext : BaseDbContext
    {
        private readonly IConfiguration _config;
        public OrderDbContext(DbContextOptions<OrderDbContext> options, IConfiguration config) : base(options)
        {
            this._config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("order");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
        }

        public override string ContextId
        {
            get
            {
                return this._config.GetValue<string>("Modules:Order:ContextId");
            }
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
