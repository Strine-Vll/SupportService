using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments")
            .HasKey(c => c.Id);

        builder.HasOne(c => c.ServiceRequest)
            .WithMany(sr => sr.Comments)
            .HasForeignKey(c => c.ServiceRequestId);
    }
}