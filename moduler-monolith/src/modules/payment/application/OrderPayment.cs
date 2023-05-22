using core_application.Abstractions;
using core_application.Common;
using core_domain;
using core_messages;
using MediatR;
using Microsoft.Extensions.Configuration;
using paymentDomain.Entities;

namespace paymentApplication
{
    public static class OrderPayment
    {
        #region Command
        public class Command : CommandBase<Result>
        {
            public Guid OrderNo { get; set; }
            public DateTime PaymentDate { get; set; } 
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IDbContextHandler _dbContextHandler;
            public CommandHandler(IEnumerable<IDbContextHandler> dbContextHandlers, IConfiguration config)
            {
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Payment:ContextId"));
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                if (this._dbContextHandler.GetDbSet<InboxMessage>().Any(x => x.ConsumerType == request.ConsumerType
                                                                          && x.MessageId == request.MessageId.ToString()))
                {
                    return Result.Success();
                }

                await this._dbContextHandler.GetDbSet<InboxMessage>().AddAsync(InboxMessage.CreateInboxMessage(request.ConsumerType, request.MessageId.ToString(), DateTime.Now));

                var newPayment = Payment.CreatePayment(request.OrderNo, request.PaymentDate);
                newPayment.AddIntegrationEvent(new PaymentSuccessed
                {
                    OrderNo = request.OrderNo
                });

                await this._dbContextHandler.GetDbSet<Payment>().AddAsync(newPayment);

                await this._dbContextHandler.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }

        #endregion
    }
}
