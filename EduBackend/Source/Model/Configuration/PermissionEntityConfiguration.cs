using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.HasOne(permission => permission.Role)
      .WithMany(role => role.Permissions)
      .HasForeignKey(permission => permission.RoleId)
      .IsRequired();

    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();

    builder.Property(entity => entity.Description)
      .IsRequired()
      .HasMaxLength(512);

    builder.Property(entity => entity.ClaimType)
      .IsRequired()
      .HasMaxLength(512);

    builder.Property(entity => entity.ClaimValue)
      .IsRequired()
      .HasMaxLength(512);

    builder.ToTable("RoleClaims");
  }
}