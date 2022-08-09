// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Contracts;

namespace Uow.Domain.Services;
public abstract class ServiceBase
{
    protected readonly IRepository Repository;
    protected readonly IMapper Mapper;
    protected readonly IHttpContextAccessor Accessor;

    private Guid? userId;

    protected ServiceBase(IRepository repository, IMapper mapper, IHttpContextAccessor accessor)
    {
        this.Repository = repository;
        this.Mapper = mapper;
        this.Accessor = accessor;
    }

    protected Guid? UserId =>
        userId ??= Accessor.HttpContext.User.Claims.Any(claim => claim.Type.Equals(Constants.Claims.Id))
            ? new Guid(Accessor.HttpContext.User.Claims.First(claim => claim.Type.Equals(Constants.Claims.Id)).Value)
            : null;
}
