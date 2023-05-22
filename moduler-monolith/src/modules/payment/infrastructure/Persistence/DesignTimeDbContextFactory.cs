using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace paymentInfrastructure.Persistence
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        public PaymentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentDbContext>();
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=modulerMonolithDb;username=admin;password=sa1234");

            return new PaymentDbContext(optionsBuilder.Options, null);
        }
    }
}
