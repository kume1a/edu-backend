using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.Property(role => role.Description)
      .IsRequired()
      .HasMaxLength(512);
    
    builder.Property(role => role.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();

    builder.HasMany(role => role.UserRoles)
      .WithOne(userRole => userRole.Role)
      .HasForeignKey(userRole => userRole.RoleId)
      .IsRequired();

    builder.HasMany(role => role.Permissions)
      .WithOne(permission => permission.Role)
      .HasForeignKey(role => role.RoleId)
      .IsRequired();
  }
}