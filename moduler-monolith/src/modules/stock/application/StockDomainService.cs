using stockDomain.Abstractions;
using stockDomain.Dtos;
using stockDomain.Entities;

namespace stockApplication
{
    public class StockDomainService : IStockDomainService
    {
        public bool IsStockAvailable(Stock stock, List<OrderItem> orderItems)
        {
            bool isStockAvailable = true;
            foreach (var orderItem in orderItems)
            {
                var stockRemainingQuantity = stock.StockProducts.Single(x => x.ProductId == orderItem.ProductId).RemainingQuantity;
                if (orderItem.Quantity > stockRemainingQuantity)
                {
                    isStockAvailable = false;
                    break;
                }
            }
            return isStockAvailable;
        }
    }
}
