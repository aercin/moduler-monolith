using core_domain;

namespace core_messages
{
    public class PaymentSuccessed : IntegrationEvent
    {
        public Guid OrderNo { get; set; }
    }
}
