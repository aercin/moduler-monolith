using core_application.Abstractions;
using core_application.Common;
using core_domain;
using core_messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockDomain.Abstractions;
using stockDomain.Entities;

namespace stockApplication
{
    public static class DecreaseStock
    {
        #region Command
        public class Command : CommandBase<Result>
        {
            public Guid OrderNo { get; set; }
            public List<stockDomain.Dtos.OrderItem> Items { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IDbContextHandler _dbContextHandler;
            private readonly IStockDomainService _stockDomainService;
            public CommandHandler(IEnumerable<IDbContextHandler> dbContextHandlers, IStockDomainService stockDomainService, IConfiguration config)
            {
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Stock:ContextId"));
                this._stockDomainService = stockDomainService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                if (this._dbContextHandler.GetDbSet<InboxMessage>().Any(x => x.ConsumerType == request.ConsumerType
                                                                          && x.MessageId == request.MessageId.ToString()))
                {
                    return Result.Success();
                }

                await this._dbContextHandler.GetDbSet<InboxMessage>().AddAsync(InboxMessage.CreateInboxMessage(request.ConsumerType, request.MessageId.ToString(), DateTime.Now));

                var stock = this._dbContextHandler.GetDbSet<Stock>().Include(x => x.StockProducts).Single();

                if (stock.DecreaseStock(request.Items, this._stockDomainService))
                {
                    stock.AddIntegrationEvent(new StockDecreased
                    {
                        OrderNo = request.OrderNo
                    });
                }
                else
                {
                    stock.AddIntegrationEvent(new StockDecreaseFailed
                    {
                        OrderNo = request.OrderNo
                    });
                }

                await this._dbContextHandler.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
        #endregion 
    }
}
