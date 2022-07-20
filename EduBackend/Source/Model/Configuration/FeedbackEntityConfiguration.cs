using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class FeedbackEntityConfiguration: IEntityTypeConfiguration<Feedback>
{
  public void Configure(EntityTypeBuilder<Feedback> builder)
  {
    builder.ToTable("Feedback");
    
    builder.Property(e => e.Content)
      .HasMaxLength(2048)
      .IsRequired();
    
    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();
  }
}