using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string[] _trackedProperties = new[] { "Title", "Description", "GroupId", "AppointedId", "StatusId" };

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions,
        IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
    {
        _httpContextAccessor = httpContextAccessor;

        try
        {
            Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override int SaveChanges()
    {
        AddAuditLogs();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddAuditLogs();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddAuditLogs()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified && e.Entity is ServiceRequest);

        foreach (var entry in entries)
        {
            var entityName = entry.Entity.GetType().Name;
            var primaryKey = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? "";

            foreach (var prop in entry.Properties)
            {
                if (prop.IsModified && _trackedProperties.Contains(prop.Metadata.Name))
                {
                    var oldValue = prop.OriginalValue?.ToString();
                    var newValue = prop.CurrentValue?.ToString();

                    if (oldValue != newValue)
                    {
                        AuditLogs.Add(new AuditLog
                        {
                            EntityName = entityName,
                            EntityId = primaryKey,
                            PropertyName = prop.Metadata.Name,
                            OldValue = oldValue,
                            NewValue = newValue,
                            ChangedAt = DateTime.UtcNow,
                            ChangedBy = GetCurrentUserId()
                        });
                    }
                }
            }
        }
    }

    private string? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;

        var userIdClaim = user.FindFirst("userId") ?? user.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim?.Value;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<ServiceRequest> ServiceRequests { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Attachment> Attachments { get; set; }

    public DbSet<Status> Statuses { get; set; }

    public DbSet<ServiceRequestStats> ServiceRequestStats { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }
}
