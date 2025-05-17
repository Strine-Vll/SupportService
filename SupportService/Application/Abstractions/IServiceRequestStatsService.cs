using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.ServiceRequestStatsDtos;

namespace Application.Abstractions;

public interface IServiceRequestStatsService
{
    Task<StatDto> GetRequestStat(int requestId);

    Task<List<StatDto>> FilterStat(FilterStatDto filter);

    Task CloseServiceRequest(int requestId, double satisfactionIndex);

    Task ReescalateRequest(int requestId);
}
