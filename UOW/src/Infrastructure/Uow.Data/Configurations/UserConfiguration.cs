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
    }
}