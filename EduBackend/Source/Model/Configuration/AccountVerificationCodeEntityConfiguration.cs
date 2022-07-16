using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class AccountVerificationCodeEntityConfiguration: IEntityTypeConfiguration<AccountVerificationCode>
{
  public void Configure(EntityTypeBuilder<AccountVerificationCode> builder)
  {
    builder.HasOne(accountVerificationCode => accountVerificationCode.User)
      .WithOne(user => user.AccountVerificationCode);

    builder.Property(entity => entity.Code)
      .HasMaxLength(5)
      .IsRequired();
  }
}