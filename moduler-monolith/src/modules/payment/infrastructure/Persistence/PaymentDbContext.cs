using core_infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using paymentDomain.Entities;

namespace paymentInfrastructure.Persistence
{
    public class PaymentDbContext : BaseDbContext
    {
        private readonly IConfiguration _config;
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options, IConfiguration config) : base(options)
        {
            this._config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentDbContext).Assembly);
        }

        public override string ContextId
        {
            get
            {
                return this._config.GetValue<string>("Modules:Payment:ContextId");
            }
        }

        public DbSet<Payment> Payments { get; set; }
    }
}
