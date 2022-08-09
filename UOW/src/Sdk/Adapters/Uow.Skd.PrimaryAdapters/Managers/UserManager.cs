// <copyright file="UserManager.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;

namespace Uow.Skd.PrimaryAdapters.Managers;

public sealed class UserManager : IUserService
{
    private readonly IUserService service;

    public UserManager(IUserService service) => this.service = service;

    public async Task<Guid> CreateAsync(UserCreateDto user, CancellationToken cancellationToken) =>
    await service.CreateAsync(user, cancellationToken).ConfigureAwait(false);

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken) =>
        await service.UpdateAsync(user, cancellationToken).ConfigureAwait(false);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken) =>
        await service.AllAsync(cancellationToken).ConfigureAwait(false);

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await service.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken) =>
        await service.AssignRole(userId, roleId, cancellationToken).ConfigureAwait(false);

    public async Task RevokeRole(Guid userId, Guid roleId, CancellationToken cancellationToken) =>
        await service.RevokeRole(userId, roleId, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<RoleDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken) =>
        await service.GetUserRolesAsync(userId, cancellationToken).ConfigureAwait(false);
}