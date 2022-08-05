namespace Uow.Domain.Contracts;

public interface IUnitOfWork
{
    IRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}