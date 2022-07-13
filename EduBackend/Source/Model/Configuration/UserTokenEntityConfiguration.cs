using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class UserTokenEntityConfiguration : IEntityTypeConfiguration<IdentityUserToken<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
  {
    builder.ToTable("UserTokens");
  }
}