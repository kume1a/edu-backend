using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class SignUpRequestEntityConfiguration : IEntityTypeConfiguration<SignUpRequest>
{
  public void Configure(EntityTypeBuilder<SignUpRequest> builder)
  {
    builder.Property(entity => entity.Email)
      .IsRequired()
      .HasMaxLength(512);

    builder.Property(entity => entity.Code)
      .IsRequired()
      .HasMaxLength(5);

    builder.Property(entity => entity.Uuid)
      .HasMaxLength(36);
  }
}