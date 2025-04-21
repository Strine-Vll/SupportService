using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
{
    public void Configure(EntityTypeBuilder<ServiceRequest> builder)
    {
        builder.ToTable("ServiceRequests")
            .HasKey(sr => sr.Id);

        builder.Property(sr => sr.UpdatedDate)
            .IsRequired(false);

        builder.HasOne(sr => sr.CreatedBy)
            .WithMany()
            .HasForeignKey(sr => sr.CreatedById)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.Appointed)
            .WithMany(u => u.AppointedRequests)
            .HasForeignKey(sr => sr.AppointedId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(sr => sr.Status)
            .WithMany()
            .HasForeignKey(sr => sr.StatusId)
            .IsRequired();

        builder.HasMany(sr => sr.Comments)
            .WithOne(c => c.ServiceRequest)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
