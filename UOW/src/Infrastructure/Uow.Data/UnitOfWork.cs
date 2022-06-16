using Uow.Domain.Contracts;

namespace Uow.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly UowContext _context;

        private IGenericRepository? _userRepository;

        public UnitOfWork(UowContext context)
        {
            _context = context;
        }

        public IGenericRepository UserRepository =>
            _userRepository ??= new GenericRepository<UowContext>(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}
