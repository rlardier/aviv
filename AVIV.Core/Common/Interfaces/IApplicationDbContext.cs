using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AVIV.Core.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        ChangeTracker ChangeTracker { get; }
        EntityEntry Entry(object entity);


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
