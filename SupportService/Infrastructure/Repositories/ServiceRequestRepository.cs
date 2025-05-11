using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ServiceRequestRepository : BaseRepository<ServiceRequest>, IServiceRequestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ServiceRequest>> GetByGroup(int groupId)
    {
        var result = await _dbContext.ServiceRequests
            .Where(sr => sr.GroupId == groupId)
            .Include(sr => sr.Status)
            .ToListAsync();

        return result;
    }

    public async Task<List<ServiceRequest>> GetPreviewByUser(int userId)
    {
        var result = await _dbContext.ServiceRequests
            .Where(sr => sr.CreatedById == userId)
            .Include(sr => sr.Status)
            .ToListAsync();

        return result;
    }

    public async Task<ServiceRequest> GetOverviewById(int requestId)
    {
        var result = await _dbContext.ServiceRequests
            .Where(sr => sr.Id == requestId)
            .Include(sr => sr.CreatedBy)
            .Include(sr => sr.Status)
            .Include(sr => sr.Appointed)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<ServiceRequest> GetRequestForUpdate(int requestId)
    {
        var result = await _dbContext.ServiceRequests
            .Where(sr => sr.Id == requestId)
            .Include(sr => sr.CreatedBy)
            .Include(sr => sr.Appointed)
            .Include(sr => sr.Status)
            //.Include(sr => sr.Comments)
            .Include(sr => sr.Group)
            .FirstOrDefaultAsync();

        return result;
    }
}
