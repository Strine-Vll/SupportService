using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ServiceRequestStatsRepository : BaseRepository<ServiceRequestStats>, IServiceRequestStatsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceRequestStatsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceRequestStats> GetByRequestIdAsync(int requestId)
    {
        var result = await _dbContext.ServiceRequestStats
            .Where(rs => rs.ServiceRequestId == requestId)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<ServiceRequestStats>> FilterStats(DateTime? startDate, DateTime? endDate, int? userId)
    {
        IQueryable<ServiceRequestStats> query = _dbContext.ServiceRequestStats;

        if (startDate.HasValue)
        {
            query = query.Where(s => s.PeriodStart >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(s => s.PeriodStart <= endDate.Value);
        }

        if (userId.HasValue)
        {
            query = query.Where(s => s.UserId == userId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task CloseRequestStatsUpdate(int requestId)
    {
        var result = await _dbContext.ServiceRequestStats
            .Where(rs => rs.ServiceRequestId == requestId)
            .FirstOrDefaultAsync();

        var logs = await _dbContext.AuditLogs
            .Where(al => al.EntityId == requestId.ToString())
            .ToListAsync();

        var firstResponceStart = await _dbContext.ServiceRequests
            .FirstOrDefaultAsync(sr => sr.Id == requestId);

        var firstResponceEnd = logs
            .OrderBy(l => l.ChangedAt)
            .FirstOrDefault(l => l.PropertyName == "StatusId" && l.OldValue == "1" && l.NewValue == "2");

        var resolutionStart = logs
            .OrderBy(l => l.ChangedAt)
            .FirstOrDefault(l => l.PropertyName == "StatusId" && l.NewValue == "2");

        var resolutionEnd = logs
            .OrderByDescending(l => l.ChangedAt)
            .FirstOrDefault(l => l.PropertyName == "StatusId" && l.NewValue == "3");

        result.PeriodStart = firstResponceEnd.ChangedAt;
        result.PeriodEnd = resolutionEnd.ChangedAt;
        result.ReactionTime = firstResponceEnd.ChangedAt - firstResponceStart.CreatedDate;
        result.ResolutionTime = resolutionEnd.ChangedAt - resolutionStart.ChangedAt;
        result.UserId = firstResponceStart.AppointedId;

        await _dbContext.SaveChangesAsync();
    }
}
