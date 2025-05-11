using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface IServiceRequestRepository : IBaseRepository<ServiceRequest>
{
    Task<List<ServiceRequest>> GetByGroup(int groupId);

    Task<List<ServiceRequest>> GetPreviewByUser(int userId);

    Task<ServiceRequest> GetOverviewById(int requestId);

    Task<ServiceRequest> GetRequestForUpdate(int requestId);
}
