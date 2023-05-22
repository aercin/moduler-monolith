using core_application.Abstractions;
using core_application.Common;
using core_domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using orderDomain.Entities;

namespace orderApplication
{
    public static class OrderFailed
    {
        #region Command
        public class Command : CommandBase<Result>
        {
            public Guid OrderNo { get; set; } 
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
                if (this._dbContextHandler.GetDbSet<InboxMessage>().Any(x => x.ConsumerType == request.ConsumerType
                                                                          && x.MessageId == request.MessageId.ToString()))
                {
                    return Result.Success();
                }

                await this._dbContextHandler.GetDbSet<InboxMessage>().AddAsync(InboxMessage.CreateInboxMessage(request.ConsumerType, request.MessageId.ToString(), DateTime.Now));

                var relatedOrder = this._dbContextHandler.GetDbSet<Order>().Single(x => x.OrderNo == request.OrderNo);

                relatedOrder.MarkOrderAsFailed();

                await this._dbContextHandler.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
        #endregion
    }
}
