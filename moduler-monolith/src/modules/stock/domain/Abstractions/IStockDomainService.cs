using stockDomain.Dtos;
using stockDomain.Entities;

namespace stockDomain.Abstractions
{
    public interface IStockDomainService
    {
        bool IsStockAvailable(Stock stock, List<OrderItem> orderItems);
    }
}
