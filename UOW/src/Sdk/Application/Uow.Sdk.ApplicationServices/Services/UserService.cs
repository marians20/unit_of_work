// <copyright file="UserService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;

namespace Uow.Sdk.ApplicationServices.Services;

public sealed class UserService : IUserService
{
    public Task<Guid> CreateAsync(UserCreateDto user, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateAsync(UserDto user, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task RevokeRole(Guid userId, Guid roleId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<IEnumerable<RoleDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}