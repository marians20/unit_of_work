using System.Linq.Expressions;

namespace Uow.Domain.Contracts;

public interface IGenericRepository
{
    Task CreateAsync<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    Task DeleteAsync<T>(Guid id, CancellationToken cancellationToken) where T : class;
    Task<IEnumerable<T>> AllAsync<T>(CancellationToken cancellationToken) where T : class;
    Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class;
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class;
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}