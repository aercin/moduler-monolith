using core_domain;

namespace core_messages
{
    public class StockDecreaseFailed : IntegrationEvent
    {
        public Guid OrderNo { get; set; }
    }
}
