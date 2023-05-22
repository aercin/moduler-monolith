using AutoMapper;
using core_application.Abstractions;
using core_application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockDomain.Entities;
using System.Runtime.CompilerServices;

namespace stockApplication
{
    public static class GetStocks
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
                this._dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Stock:ContextId"));
                this._mapper = mapper;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = this._dbContextHandler.GetDbSet<Stock>().Include(x => x.StockProducts).Single();

                return Result<List<Product>>.Success(this._mapper.Map<List<Product>>(result.StockProducts)); 
            }
        }
        #endregion

        #region Mapping Profile
        public class QueryStockProfile : Profile
        {
            public QueryStockProfile()
            {
                CreateMap<StockProduct, Product>();
            }
        }

        #endregion

        #region Response  
        public class Product
        {
            public int ProductId { get; set; }
            public int InitialQuantity { get; set; }
            public int RemainingQuantity { get; set; }
        }
        #endregion
    }
}
