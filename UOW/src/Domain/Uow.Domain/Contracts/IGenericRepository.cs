using System.Linq.Expressions;

namespace Uow.Domain.Contracts;

public interface IGenericRepository
{
    Task Create<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    Task Delete<T>(Guid id, CancellationToken cancellationToken) where T : class;
    Task<IEnumerable<T>> All<T>(CancellationToken cancellationToken) where T : class;
    Task<T?> GetById<T>(Guid id, CancellationToken cancellationToken) where T : class;
    Task<bool> Any<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class;
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}