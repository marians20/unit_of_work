using Uow.Domain.Contracts;

namespace Uow.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly UowContext _context;

    private IGenericRepository? _userRepository;

    public UnitOfWork(UowContext context, IGenericRepository userRepository)
    {
        _context = context;
        Users = userRepository;
    }

    public IGenericRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}