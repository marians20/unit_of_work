// <copyright file="UserService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Entities;
using Uow.Domain.Exceptions;
using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;
using Uow.SecondaryPorts;

namespace Uow.ApplicationServices.Services;

public sealed class UserService : ServiceBase, IUserService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="accessor"></param>
    /// <param name="repository"></param>
    public UserService(IRepository repository, IMapper mapper, IHttpContextAccessor accessor)
    : base(repository, mapper, accessor)
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

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await Task.FromResult(Repository.Query<User>(u => u.Id == id, usr => usr.Roles).SingleOrDefault())
            .ConfigureAwait(false);

        EnsureEntityExists(user);
        return Mapper.Map<UserDto>(user);
    }

    public async Task AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var user = await Repository.GetByIdAsync<User>(userId, cancellationToken).ConfigureAwait(false);
        EnsureEntityExists(user);

        var role = await Repository.GetByIdAsync<Role>(roleId, cancellationToken).ConfigureAwait(false);
        EnsureEntityExists(role);

        user!.Roles.Add(role!);

        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task RevokeRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var user = Repository.Query<User>(u => u.Id == userId, usr => usr.Roles).SingleOrDefault();
        EnsureEntityExists(user);

        if (user!.Roles.All(r => r.Id != roleId))
        {
            throw new EntityNotFoundException(nameof(Role));
        }

        user.Roles.Remove(user.Roles.SingleOrDefault(r => r.Id != roleId)!);
        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<RoleDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await Task.FromResult(Repository.Query<User>(u => u.Id == userId, usr => usr.Roles).SingleOrDefault())
            .ConfigureAwait(false);

        EnsureEntityExists(user);
        return Mapper.Map<IEnumerable<RoleDto>>(user!.Roles);
    }
}