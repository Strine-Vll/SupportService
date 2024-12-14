using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles")
            .HasKey(uc => uc.Id);

        builder.HasData(
        new[]
            {
                new UserRole { Id = 1, RoleName = "Анонимный пользователь" },
                new UserRole { Id = 2, RoleName = "Пользователь" },
                new UserRole { Id = 3, RoleName = "Специалист поддержки" },
                new UserRole { Id = 4, RoleName = "Менеджер" }
           });
    }
}
