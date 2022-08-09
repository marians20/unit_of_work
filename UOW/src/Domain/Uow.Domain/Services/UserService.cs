using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;

namespace Uow.Domain.Services;

public sealed class UserService : ServiceBase, IUserService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="accessor"></param>
    /// <param name="repository"></param>
    public UserService(IRepository repository, IMapper mapper, IHttpContextAccessor accessor)
    :base(repository, mapper, accessor)
    {
    }

    public async Task<Guid> CreateAsync(UserCreateDto user, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<User>(user);
        entity.Id = Guid.NewGuid();
        entity.AddCreationTrackingInfo(UserId);
        await Repository.CreateAsync(entity);
        await Repository.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<User>(user);
        entity.AddUpdatingTrackingInfo(UserId);
        Repository.Update(entity);
        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await Repository.DeleteAsync<User>(id, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken) =>
        Mapper.Map<IEnumerable<UserDto>>(await Repository.AllAsync<User>(cancellationToken));

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Mapper.Map<UserDto>(await Repository.GetByIdAsync<User>(id, cancellationToken));
}