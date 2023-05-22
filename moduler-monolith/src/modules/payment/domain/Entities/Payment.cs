using core_domain;

namespace paymentDomain.Entities
{
    public class Payment : AggregateRootBase
    {
        public int Id { get; private set; }
        public Guid OrderNo { get; private set; }
        public DateTime PaymentDate { get; private set; }

        private Payment()
        {

        }

        private Payment(Guid orderNo, DateTime paymentDate)
        {
            this.OrderNo = orderNo;
            this.PaymentDate = paymentDate;
        }

        public static Payment CreatePayment(Guid orderNo, DateTime paymentDate)
        {
            return new Payment(orderNo, paymentDate);
        }
    }
}
