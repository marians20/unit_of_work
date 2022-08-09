// <copyright file="RoleManager.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;

namespace Uow.Skd.PrimaryAdapters.Managers;
internal class RoleManager: IRoleService
{
    private readonly IRoleService service;

    public RoleManager(IRoleService service) => this.service = service;

    public async Task<Guid> CreateAsync(RoleCreateDto role, CancellationToken cancellationToken) =>
        await service.CreateAsync(role, cancellationToken).ConfigureAwait(false);

    public async Task UpdateAsync(RoleDto role, CancellationToken cancellationToken) =>
        await service.UpdateAsync(role, cancellationToken).ConfigureAwait(false);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<RoleDto>> AllAsync(CancellationToken cancellationToken) =>
        await service.AllAsync(cancellationToken).ConfigureAwait(false);

    public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await service.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
}
