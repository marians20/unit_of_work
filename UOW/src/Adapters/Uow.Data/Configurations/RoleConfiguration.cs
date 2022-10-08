// <copyright file="RoleConfiguration.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uow.Domain.Entities;

namespace Uow.Data.Configurations;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(x => x.Id);

        builder.Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
