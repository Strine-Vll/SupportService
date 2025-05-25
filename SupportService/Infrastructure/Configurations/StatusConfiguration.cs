using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Statuses")
            .HasKey(c => c.Id);

        builder.Property(c => c.StatusName).IsRequired();

        builder.HasData(
        new[]
            {
                new Status { Id = 1, StatusName = "Новое" },
                new Status { Id = 2, StatusName = "В работе" },
                new Status { Id = 3, StatusName = "Решено" },
                new Status { Id = 5, StatusName = "На доработку" },
                new Status { Id = 6, StatusName = "Закрыто" }
           });
    }
}
