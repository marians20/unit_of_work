using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;

namespace Uow.Domain.Services;

public sealed class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor accessor;
    private readonly IRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="accessor"></param>
    /// <param name="repository"></param>
    public UserService(IMapper mapper, IHttpContextAccessor accessor, IRepository repository)
    {
        this.mapper = mapper;
        this.accessor = accessor;
        this.repository = repository;
    }

    public async Task<Guid> CreateAsync(UserCreateDto user, CancellationToken cancellationToken)
    {
        var userEntity = mapper.Map<User>(user);
        userEntity.Id = Guid.NewGuid();
        await repository.CreateAsync(AddCreationTrackingInfo(userEntity));
        await repository.SaveChangesAsync(cancellationToken);
        return userEntity.Id;
    }

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
    {
        repository.Update(AddUpdatingTrackingInfo(mapper.Map<User>(user)));
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync<User>(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken) =>
        mapper.Map<IEnumerable<UserDto>>(await repository.AllAsync<User>(cancellationToken));

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        mapper.Map<UserDto>(await repository.GetByIdAsync<User>(id, cancellationToken));

    private T AddCreationTrackingInfo<T>(T entity) where T: EntityWithTracking
    {
        if (accessor.HttpContext.User.Claims.Any(claim => claim.Type.Equals(Constants.Claims.Id)))
        {
            entity.CreatedBy = new Guid(accessor.HttpContext.User.Claims
                .First(claim => claim.Type.Equals(Constants.Claims.Id)).Value);
        }

        entity.CreationDate = DateTime.Now;

        return entity;
    }

    private T AddUpdatingTrackingInfo<T>(T entity) where T : EntityWithTracking
    {
        if (accessor.HttpContext.User.Claims.Any(claim => claim.Type.Equals(Constants.Claims.Id)))
        {
            entity.ModifiedBy = new Guid(accessor.HttpContext.User.Claims
                .First(claim => claim.Type.Equals(Constants.Claims.Id)).Value);
        }

        entity.ModifiedDate = DateTime.Now;

        return entity;
    }
}