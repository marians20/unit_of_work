// <copyright file="ServiceBase.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.ApplicationServices.Specifications;
using Uow.Domain;
using Uow.Domain.Exceptions;
using Uow.SecondaryPorts;

namespace Uow.ApplicationServices.Services;
public abstract class ServiceBase
{
    protected readonly IRepository Repository;
    protected readonly IMapper Mapper;
    protected readonly IHttpContextAccessor Accessor;
    private readonly ClaimByTypeSpecification claimByType;

    private Guid? userId;

    protected ServiceBase(IRepository repository, IMapper mapper, IHttpContextAccessor accessor)
    {
        Repository = repository;
        Mapper = mapper;
        Accessor = accessor;
        claimByType = new ClaimByTypeSpecification(Constants.Claims.Id);
    }

    protected Guid? UserId =>
        userId ??= Accessor.HttpContext.User.Claims.Any(claimByType)
            ? new Guid(Accessor.HttpContext.User.Claims.First(claimByType).Value)
            : null;

    protected static void EnsureEntityExists<T>(T entity)
    {
        if (entity == null)
        {
            throw new EntityNotFoundException(nameof(T));
        }
    }
}
