// <copyright file="UserConfiguration.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uow.Domain.Entities;

namespace Uow.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(e => e.Id);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow);

        builder
            .HasMany(s => s.Roles)
            .WithMany(c => c.Users)
            .UsingEntity<UserRole>(j =>
            {
                j.ToTable("UserRoles").HasKey(x => new {x.UserId, x.RoleId});
                j.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.RoleId);
                j.HasOne(ur => ur.Role).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });

        builder.OwnsOne(
            x => x.Address,
            bld =>
            {
                bld.ToTable("Addresses");
                bld.Property<Guid>("Id");
                bld.HasKey("Id");
                bld.OwnsOne(x => x.City);
                bld.OwnsOne(x => x.Country);
                bld.OwnsOne(x => x.County);
                bld.OwnsOne(x => x.PostalCode);
                bld.OwnsOne(x => x.FirstLine);
                bld.OwnsOne(x => x.SecondLine);
            });
    }
}