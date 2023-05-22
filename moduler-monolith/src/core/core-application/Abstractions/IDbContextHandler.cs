using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace core_application.Abstractions
{
    public interface IDbContextHandler
    {
        string ContextId { get; }
        DbSet<T> GetDbSet<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
