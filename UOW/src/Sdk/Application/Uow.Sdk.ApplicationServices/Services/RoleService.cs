// <copyright file="RoleService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;
using Uow.SecondaryPorts;

namespace Uow.Sdk.ApplicationServices.Services;
public sealed class RoleService : IRoleService
{
    private readonly IRepository repository;

    public RoleService(IRepository repository) => this.repository = repository;

    public Task<Guid> CreateAsync(RoleCreateDto role, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateAsync(RoleDto role, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<IEnumerable<RoleDto>> AllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}
