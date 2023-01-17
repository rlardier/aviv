using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AVIV.SharedKernel.Interface;

namespace AVIV.Infrastructure.Data
{

    public class EFReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public EFReadRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
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
