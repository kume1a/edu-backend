using EduBackend.Source.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Model;

public class DataContext : IdentityDbContext<User, Role, long,
  IdentityUserClaim<long>, UserRole,
  IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
  public DbSet<Permission> Permissions { get; set; }
  public DbSet<RolePermission> RolePermissions { get; set; }

  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.UseSerialColumns();

    modelBuilder.Entity<User>()
      .HasMany(user => user.UserRoles)
      .WithOne(userRole => userRole.User)
      .HasForeignKey(userRole => userRole.UserId)
      .IsRequired();

    modelBuilder.Entity<Role>()
      .HasMany(role => role.UserRoles)
      .WithOne(userRole => userRole.Role)
      .HasForeignKey(userRole => userRole.RoleId)
      .IsRequired();

    modelBuilder.Entity<Role>()
      .HasMany(role => role.RolePermissions)
      .WithOne(rolePermission => rolePermission.Role)
      .HasForeignKey(rolePermission => rolePermission.RoleId)
      .IsRequired();

    modelBuilder.Entity<Permission>()
      .HasMany(permission => permission.RolePermissions)
      .WithOne(rolePermission => rolePermission.Permission)
      .HasForeignKey(rolePermission => rolePermission.PermissionId)
      .IsRequired();
  }
}