using core_messages;
using MassTransit;
using MediatR;
using stockApplication;

namespace stockApi.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlaced>
    {
        private readonly IMediator _mediator;
        public OrderPlacedConsumer(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderPlaced> context)
        {
            await this._mediator.Send(new DecreaseStock.Command
            {
                MessageId = context.Message.Id,
                ConsumerType = this.GetType().Name,
                OrderNo = context.Message.OrderNo,
                Items = context.Message.Items.Select(x => new stockDomain.Dtos.OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            });
        }
    }
}
