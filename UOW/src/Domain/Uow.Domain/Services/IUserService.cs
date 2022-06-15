using Uow.Domain.Dtos;

namespace Uow.Domain.Services;

public interface IUserService
{
    Task<Guid> CreateAsync(UserDto user, CancellationToken cancellationToken);
    Task UpdateAsync(UserDto user, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken);
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}