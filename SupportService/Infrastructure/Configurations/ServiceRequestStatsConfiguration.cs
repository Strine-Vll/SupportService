using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class ServiceRequestStatsConfiguration : IEntityTypeConfiguration<ServiceRequestStats>
{
    public void Configure(EntityTypeBuilder<ServiceRequestStats> builder)
    {
        builder.ToTable("ServiceRequestStats")
            .HasKey(rs => rs.Id);

        builder.HasOne(rs => rs.User)
            .WithOne()
            .HasForeignKey<ServiceRequestStats>(rs => rs.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(rs => rs.ServiceRequest)
            .WithOne(s => s.ServiceRequestStats)
            .HasForeignKey<ServiceRequestStats>(rs => rs.ServiceRequestId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
