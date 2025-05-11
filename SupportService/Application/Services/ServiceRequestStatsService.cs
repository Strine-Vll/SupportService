using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Abstractions;

namespace Application.Services;

public class ServiceRequestStatsService : IServiceRequestStatsService
{
    private readonly IServiceRequestStatsRepository _serviceRequestStatsRepository;

    public ServiceRequestStatsService(IServiceRequestStatsRepository serviceRequestStatsRepository)
    {
        _serviceRequestStatsRepository = serviceRequestStatsRepository;        
    }

    public async Task CloseServiceRequest(int requestId, double satisfactionIndex)
    {
        var result = await _serviceRequestStatsRepository.GetByRequestIdAsync(requestId);

        if (result == null)
        {
            return;
        }

        result.SatisfactionIndex = satisfactionIndex;

        await _serviceRequestStatsRepository.UpdateAsync(result);
    }
}
