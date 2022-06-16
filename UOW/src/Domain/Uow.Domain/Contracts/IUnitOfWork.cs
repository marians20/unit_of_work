namespace Uow.Domain.Contracts;

public interface IUnitOfWork
{
    IGenericRepository UserRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}