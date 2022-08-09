// <copyright file="DiConfiguration.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Uow.Data.Extensions;
using Uow.SecondaryPorts;

namespace Uow.Data;

/// <summary>
/// DiConfiguration
/// </summary>
public static class DiConfiguration
{
    public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<UowContext>(opts =>
            {
                var connectionString = configuration.GetConnectionString("default").SolveSpecialFolder();

                opts.UseSqlite(connectionString, a =>
                {
                    a.MigrationsAssembly(typeof(UowContext).Assembly.FullName);
                });
            })
            .AddScoped<IRepository, Repository<UowContext>>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
}