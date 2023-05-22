using stockDomain.Entities;

namespace stockInfrastructure.Persistence
{
    public static class StockDbContextSeed
    {
        public static void SeedData(this StockDbContext context)
        {
            if (!context.Set<Stock>().Any())
            {
                context.Set<Stock>().Add(Stock.CreateStock(new List<StockProduct>
                    {
                        StockProduct.CreateStockProduct(1,100,100),
                        StockProduct.CreateStockProduct(2,50,50),
                        StockProduct.CreateStockProduct(3,25,25),
                    }));
                context.SaveChanges();
            }
        }
    }
}
