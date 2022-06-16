namespace Uow.Domain.Contracts;

public interface IUnitOfWork
{
    IGenericRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}