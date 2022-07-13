using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasMany(user => user.UserRoles)
      .WithOne(userRole => userRole.User)
      .HasForeignKey(userRole => userRole.UserId)
      .IsRequired();

    builder.Property(entity => entity.Username)
      .IsRequired()
      .HasMaxLength(512);

    builder.Property(entity => entity.Email)
      .IsRequired()
      .HasMaxLength(512);

    builder.Property(entity => entity.UserName)
      .IsRequired()
      .HasMaxLength(512);
  }
}