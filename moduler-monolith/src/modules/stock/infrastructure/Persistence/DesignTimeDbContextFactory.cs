using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace stockInfrastructure.Persistence
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StockDbContext>
    {
        public StockDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=modulerMonolithDb;username=admin;password=sa1234");

            return new StockDbContext(optionsBuilder.Options, null);
        }
    }
}
