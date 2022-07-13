using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class UserClaimsEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
  {
    builder.ToTable("UserClaims");
  }
}