using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Dtos.ServiceRequestStatsDtos;
using AutoMapper;
using Domain.Abstractions;

namespace Application.Services;

public class ServiceRequestStatsService : IServiceRequestStatsService
{
    private readonly IServiceRequestStatsRepository _serviceRequestStatsRepository;

    private readonly IMapper _mapper;

    public ServiceRequestStatsService(IServiceRequestStatsRepository serviceRequestStatsRepository, IMapper mapper)
    {
        _serviceRequestStatsRepository = serviceRequestStatsRepository;
        _mapper = mapper;
    }

    public async Task<StatDto> GetRequestStat(int requestId)
    {
        var dbStat = await _serviceRequestStatsRepository.GetByRequestIdAsync(requestId);

        var result = _mapper.Map<StatDto>(dbStat);

        return result;
    }

    public async Task<List<StatDto>> FilterStat(FilterStatDto filter)
    {
        var dbStat = await _serviceRequestStatsRepository.FilterStats(filter.StartDate, filter.EndDate, filter.UserId);

        var result = _mapper.Map<List<StatDto>>(dbStat);

        return result;
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
        await _serviceRequestStatsRepository.CloseRequestStatsUpdate(requestId);
    }

    public async Task ReescalateRequest(int requestId)
    {
        var result = await _serviceRequestStatsRepository.GetByRequestIdAsync(requestId);

        if (result == null)
        {
            return;
        }

        result.ReescalateAmount += 1;

        await _serviceRequestStatsRepository.UpdateAsync(result);
    }
}
