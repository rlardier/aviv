using Ardalis.Specification;

namespace AVIV.SharedKernel.Interface
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        void TrackEntity<TEntity>(TEntity entity);
        IQueryable<T> AsQueryable();
        IQueryable<T> AsQueryable(ISpecification<T> specification);
        IQueryable<TResult> AsQueryable<TResult>(ISpecification<T, TResult> specification);
    }
}
