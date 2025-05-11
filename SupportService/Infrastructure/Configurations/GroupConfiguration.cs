using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups")
            .HasKey(sr => sr.Id);

        builder.HasMany(g => g.ServiceRequests)
            .WithOne(sr => sr.Group)
            .HasForeignKey(sr => sr.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.Users)
            .WithMany(u => u.Groups);
    }
}