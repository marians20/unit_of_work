﻿using Microsoft.Extensions.DependencyInjection;
using Uow.Domain.Contracts;
using Uow.Domain.Mappers;
using Uow.Domain.Services;

namespace Uow.Domain;

public static class DiConfiguration
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services) =>
        services
            .AddAutoMapper(typeof(UowMapperProfile))
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>();
}