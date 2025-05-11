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
}
