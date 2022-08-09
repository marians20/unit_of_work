// <copyright file="IRoleService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.PrimaryPorts.Dtos;

namespace Uow.PrimaryPorts;

public interface IRoleService
{
    Task<Guid> CreateAsync(RoleCreateDto role, CancellationToken cancellationToken);

    Task UpdateAsync(RoleDto role, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<RoleDto>> AllAsync(CancellationToken cancellationToken);

    Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}