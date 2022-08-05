using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;
using Uow.Domain.Extensions;

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

    public async Task<Guid> CreateAsync(UserDto user, CancellationToken cancellationToken)
    {
        user.Id = Guid.NewGuid();
        var requestId = accessor.HttpContext.Request.Headers[Constants.RequestsConstants.Headers.XRequestId];
        await repository.CreateAsync(mapper.Map<User>(user));
        await repository.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
    {
        repository.Update(mapper.Map<User>(user));
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync<User>(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken)
    {
        var requestId = accessor.HttpContext.Request.GetRequestId();
        return mapper.Map<IEnumerable<UserDto>>(await repository.AllAsync<User>(cancellationToken));
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        mapper.Map<UserDto>(await repository.GetByIdAsync<User>(id, cancellationToken));
}