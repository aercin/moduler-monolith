using core_application.Abstractions;
using core_domain;
using core_domain.Entitites;
using core_infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace core_infrastructure.persistence
{
    public class BaseDbContext : DbContext, IDbContextHandler
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }

        public virtual string ContextId { get; }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.StoreEventsAsync();

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
