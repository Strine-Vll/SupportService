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
            .ToListAsync();

        return result;
    }
}
