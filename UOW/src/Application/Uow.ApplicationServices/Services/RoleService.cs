// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Entities;
using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;
using Uow.SecondaryPorts;

namespace Uow.ApplicationServices.Services;
public sealed class RoleService : ServiceBase, IRoleService
{
    public RoleService(IRepository repository, IMapper mapper, IHttpContextAccessor accessor) : base(repository, mapper, accessor)
    {
    }

    public async Task<Guid> CreateAsync(RoleCreateDto role, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<Role>(role);
        entity.Id = Guid.NewGuid();
        entity.AddCreationTrackingInfo(UserId);
        await Repository.CreateAsync(entity);
        await Repository.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(RoleDto role, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<Role>(role);
        entity.AddUpdatingTrackingInfo(UserId);
        Repository.Update(entity);
        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await Repository.DeleteAsync<Role>(id, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<RoleDto>> AllAsync(CancellationToken cancellationToken) =>
        Mapper.Map<IEnumerable<RoleDto>>(await Repository.AllAsync<Role>(cancellationToken));

    public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Mapper.Map<RoleDto>(await Repository.GetByIdAsync<Role>(id, cancellationToken));
}
