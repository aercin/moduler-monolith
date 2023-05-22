using core_domain;

namespace core_messages
{
    public class OrderPlaced : IntegrationEvent
    {
        public Guid OrderNo { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
