using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments")
            .HasKey(a => a.Id);

        builder.HasOne(a => a.Comment)
            .WithMany(c => c.Attachments)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
