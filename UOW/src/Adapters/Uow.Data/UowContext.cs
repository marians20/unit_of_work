﻿// <copyright file="UowContext.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Uow.Data.Configurations;
using Uow.Domain.Entities.Abstractions;

namespace Uow.Data;

public class UowContext : DbContext
{
    public UowContext(DbContextOptions options) : base(options) => EnsureThatDatabaseDirectoryIsCreated();

    private void EnsureThatDatabaseDirectoryIsCreated()
    {
        if (!Database.IsRelational())
        {
            return;
        }

        var csBuilder = new SqliteConnectionStringBuilder(Database.GetConnectionString());
        var dbDirectory = Path.GetDirectoryName(csBuilder.DataSource);
        if (!string.IsNullOrEmpty(dbDirectory) && !Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        RegisterAllEntities<EntityBase>(modelBuilder, typeof(EntityBase).Assembly);
        //RegisterAllEntities<ValueObject>(modelBuilder, typeof(ValueObject).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    private static void RegisterAllEntities<TBase>(ModelBuilder modelBuilder, params Assembly[] assemblies) =>
        assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TBase).IsAssignableFrom(c))
            .ToList()
            .ForEach(type => modelBuilder.Entity(type));
}