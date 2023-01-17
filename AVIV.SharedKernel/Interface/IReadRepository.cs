using Ardalis.Specification;

namespace AVIV.SharedKernel.Interface
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
        IQueryable<T> AsQueryable();
        IQueryable<T> AsQueryable(ISpecification<T> specification);
        IQueryable<TResult> AsQueryable<TResult>(ISpecification<T, TResult> specification);
    }
}
