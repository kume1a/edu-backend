using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class UserLoginsEntityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
  {
    builder.ToTable("UserLogins");
  }
}