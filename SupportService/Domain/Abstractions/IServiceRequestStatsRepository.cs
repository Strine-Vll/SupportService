using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IServiceRequestStatsRepository : IBaseRepository<ServiceRequestStats>
{
    Task<List<ServiceRequestStats>> FilterStats(DateTime? startDatte, DateTime? endDate, int? userId);

    Task<ServiceRequestStats> GetByRequestIdAsync(int requestId);
}
