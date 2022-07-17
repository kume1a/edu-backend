using System.Reflection;
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
  public DbSet<RecoverPasswordRequest> RecoverPasswordRequests { get; set; }
  public DbSet<RefreshToken> RefreshTokens { get; set; }
  public DbSet<AccountVerificationCode> AccountVerificationCodes { get; set; }
  public DbSet<Document> Documents { get; set; }
  public DbSet<DocumentParagraph> DocumentParagraphs { get; set; }
  public DbSet<Genre> Genres { get; set; }

  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.UseSerialColumns();

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}