using core_domain;

namespace core_messages
{
    public class StockDecreased : IntegrationEvent
    {
        public Guid OrderNo { get; set; }
    }
}
