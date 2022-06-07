using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Uow.Domain.Contracts;

namespace Uow.Data
{
    public class GenericRepository<TContext> : IGenericRepository where TContext: DbContext
    {
        private readonly TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public async Task Create<T>(T entity) where T : class =>
            await Set<T>().AddAsync(entity);

        public void Update<T>(T entity) where T : class =>
            _context.Entry(entity).State = EntityState.Modified;

        public async Task Delete<T>(Guid id, CancellationToken cancellationToken) where T : class
        {
            var entity = await GetById<T>(id, cancellationToken);
            if (entity is null)
            {
                return;
            }

            Set<T>().Remove(entity);
        }

        private DbSet<T> Set<T>() where T: class => _context.Set<T>();

        public async Task<IEnumerable<T>> All<T>(CancellationToken cancellationToken) where T : class =>
            await Set<T>().ToListAsync(cancellationToken: cancellationToken);

        public async Task<T?> GetById<T>(Guid id, CancellationToken cancellationToken) where T : class =>
            await Set<T>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        public async Task<bool> Any<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class =>
            await Set<T>().AnyAsync(predicate, cancellationToken);

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class =>
            Set<T>().Where(predicate);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
    }
}
