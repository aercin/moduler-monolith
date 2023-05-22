using core_application.Abstractions;
using core_application.Common;
using core_domain;
using core_messages;
using MediatR;
using Microsoft.Extensions.Configuration;
using orderDomain.Entities;

namespace orderApplication
{
    public static class PlaceOrder
    {
        #region Command
        public class Command : CommandBase<Result>
        {
            public List<orderDomain.Dtos.OrderItem> OrderItems { get; set; }
        }
        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IDbContextHandler _dbContextHandler;
            public CommandHandler(IEnumerable<IDbContextHandler> dbContextHandlers, IConfiguration config)
            {
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Order:ContextId"));
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var newOrder = Order.PlaceOrder(request.OrderItems);

                newOrder.AddIntegrationEvent(new OrderPlaced
                {
                    OrderNo = newOrder.OrderNo,
                    Items = newOrder.Products.Select(x => new OrderItem
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    }).ToList()
                });

                await this._dbContextHandler.GetDbSet<Order>().AddAsync(newOrder);

                await this._dbContextHandler.SaveChangesAsync(cancellationToken);

                return Result<Response>.Success(new Response { OrderNo = newOrder.OrderNo });
            }
        }
        #endregion

        #region Response
        public class Response
        {
            public Guid OrderNo { get; set; }
        }
        #endregion
    }
}
