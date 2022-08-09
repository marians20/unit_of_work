// <copyright file="IUserService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using Uow.Domain.Dtos;

namespace Uow.Domain.Contracts;

public interface IUserService
{
    Task<Guid> CreateAsync(UserCreateDto user, CancellationToken cancellationToken);

    Task UpdateAsync(UserDto user, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken);

    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken);

    Task RevokeRole(Guid userId, Guid roleId, CancellationToken cancellationToken);

    Task<IEnumerable<RoleDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
}