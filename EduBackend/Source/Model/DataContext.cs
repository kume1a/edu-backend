using EduBackend.Source.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Model;

public class DataContext : IdentityDbContext<User, Role, long,
  IdentityUserClaim<long>, UserRole,
  IdentityUserLogin<long>, Permission, IdentityUserToken<long>>
{
  public DbSet<Permission> Permissions { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<User> Users { get; set; }

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

    modelBuilder.Entity<Permission>()
      .HasOne(permission => permission.Role)
      .WithMany(role => role.Permissions)
      .HasForeignKey(permission => permission.RoleId)
      .IsRequired();

    modelBuilder.Entity<Permission>()
      .Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();
    
    modelBuilder.Entity<Role>()
      .HasMany(role => role.Permissions)
      .WithOne(permission => permission.Role)
      .HasForeignKey(role => role.RoleId)
      .IsRequired();
  }
}