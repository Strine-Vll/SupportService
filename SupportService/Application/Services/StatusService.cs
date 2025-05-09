using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services;

public class StatusService : IStatusService
{
    private readonly IMapper _mapper;

    private readonly IStatusRepository _statusRepository;

    public StatusService(IMapper mapper, IStatusRepository statusRepository)
    {
        _mapper = mapper;
        _statusRepository = statusRepository;
    }

    public async Task<List<Status>> GetStatusesAsync()
    {
        var statuses = await _statusRepository.GetAllAsync();

        return statuses;
    }
}
