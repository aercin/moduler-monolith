using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace orderInfrastructure.Persistence
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=modulerMonolithDb;username=admin;password=sa1234");

            return new OrderDbContext(optionsBuilder.Options, null);
        }
    }
}
