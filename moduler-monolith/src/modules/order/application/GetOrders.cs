using AutoMapper;
using core_application.Abstractions;
using core_application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using orderDomain.Entities;
using orderDomain.Enums;

namespace orderApplication
{
    public static class GetOrders
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
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Order:ContextId"));
                this._mapper = mapper;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = this._dbContextHandler.GetDbSet<Order>().QueryResource<Order, OrderItem>(_mapper, request.PageNumber, request.PageSize);

                return await Task.FromResult(result);
            }
        }
        #endregion

        #region Mapping Profile
        public class QueryOrderProfile : Profile
        {
            public QueryOrderProfile()
            {
                CreateMap<Order, OrderItem>();
            }
        }

        #endregion

        #region Response 

        public class OrderItem
        {
            public int Id { get; set; }
            public Guid OrderNo { get; set; }
            public decimal TotalPrice { get; set; }
            public OrderStatus OrderStatus { get; set; }
        }

        #endregion
    }
}
