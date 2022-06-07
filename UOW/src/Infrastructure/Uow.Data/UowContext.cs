using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Uow.Data.Configurations;
using Uow.Domain.Entities;

namespace Uow.Data;

public class UowContext : DbContext
{
    public UowContext(DbContextOptions options) : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        RegisterAllEntities<BaseEntity>(modelBuilder, typeof(BaseEntity).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    private static void RegisterAllEntities<TBase>(ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TBase).IsAssignableFrom(c))
            .ToList()
            .ForEach(type => modelBuilder.Entity(type));
    }
}