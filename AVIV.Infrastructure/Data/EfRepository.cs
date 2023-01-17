using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AVIV.SharedKernel.Interface;

namespace AVIV.Infrastructure.Data
{
    // inherit from Ardalis.Specification type
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            //_dbContext.ChangeTenant(tenantList.Last());
        }

        public void TrackEntity<TEntity>(TEntity entity)
        {
            _dbContext.ChangeTracker.TrackGraph(entity, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Unchanged;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }
            });
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> AsQueryable(ISpecification<T> specification)
        {
            var queryResult = ApplySpecification(specification);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).AsQueryable<T>();
        }

        public IQueryable<TResult> AsQueryable<TResult>(ISpecification<T, TResult> specification)
        {
            var queryResult = ApplySpecification(specification);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).AsQueryable<TResult>();
        }
    }
}
