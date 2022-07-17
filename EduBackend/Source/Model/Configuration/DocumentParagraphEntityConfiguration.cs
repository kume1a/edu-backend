using EduBackend.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduBackend.Source.Model.Configuration;

public class DocumentParagraphEntityConfiguration: IEntityTypeConfiguration<DocumentParagraph>
{
  public void Configure(EntityTypeBuilder<DocumentParagraph> builder)
  {
    builder.HasOne(documentParagraph => documentParagraph.Document)
      .WithMany(document => document.Paragraphs);

    builder.Property(entity => entity.Title)
      .HasMaxLength(512)
      .IsRequired();
    
    builder.Property(entity => entity.Content)
      .HasMaxLength(4096)
      .IsRequired();

    builder.Property(e => e.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAdd();
    
    builder.Property(e => e.UpdatedAt)
      .HasDefaultValueSql("NOW()")
      .ValueGeneratedOnAddOrUpdate();
  }
}