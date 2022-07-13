using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class RecoverPasswordRequestEntityConfiguration: IEntityTypeConfiguration<RecoverPasswordRequest>
{
  public void Configure(EntityTypeBuilder<RecoverPasswordRequest> builder)
  {
    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();

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