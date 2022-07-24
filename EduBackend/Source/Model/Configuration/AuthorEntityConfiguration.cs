using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class AuthorEntityConfiguration: IEntityTypeConfiguration<Author>
{
  public void Configure(EntityTypeBuilder<Author> builder)
  {
    builder.ToTable("Authors");

    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();
    
    builder.Property(e => e.Name)
      .HasMaxLength(256)
      .IsRequired();

    builder.Property(e => e.ImagePath)
      .HasMaxLength(2048)
      .IsRequired();
    
    builder.Property(e => e.BlurImagePath)
      .HasMaxLength(2048)
      .IsRequired();
  }
}