using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class DocumentEntityConfiguration: IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
    builder.HasMany(document => document.Paragraphs)
      .WithOne(documentParagraph => documentParagraph.Document);

    builder.Property(entity => entity.Title)
      .HasMaxLength(512)
      .IsRequired();
    
    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();
    
    builder.Property(e => e.UpdatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAddOrUpdate();
  }
}