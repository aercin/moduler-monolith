using AutoMapper;
using core_application.Abstractions;
using core_application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using paymentDomain.Entities;

namespace paymentApplication
{
    public static class GetOrderPayments
    {
        #region Query
        public class Query : QueryPaginationBase<Result>
        {
        }
        #endregion

        #region Query Handler
        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly IDbContextHandler _dbContextHandler;
            private readonly IMapper _mapper;
            public QueryHandler(IEnumerable<IDbContextHandler> dbContextHandlers, IMapper mapper, IConfiguration config)
            {
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Payment:ContextId"));
                this._mapper = mapper;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = this._dbContextHandler.GetDbSet<Payment>().QueryResource<Payment, PaymentItem>(_mapper, request.PageNumber, request.PageSize);

                return await Task.FromResult(result);
            }
        }
        #endregion


        #region Mapping Profile
        public class QueryPaymentProfile : Profile
        {
            public QueryPaymentProfile()
            {
                CreateMap<Payment, PaymentItem>();
            }
        }
        #endregion

        #region Response  
        public class PaymentItem
        {
            public Guid OrderNo { get; set; }
            public DateTime PaymentDate { get; set; }
        }
        #endregion
    }
}
