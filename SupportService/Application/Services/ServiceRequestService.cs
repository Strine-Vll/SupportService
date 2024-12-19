using Application.Abstractions;
using Application.Dtos.ServiceRequestDtos;
using Application.Exceptions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class ServiceRequestService : IServiceRequestService
{
    public ServiceRequestService(IMapper mapper, IServiceRequestRepository serviceRequestRepository)
    {
        _mapper = mapper;
        _serviceRequestRepository = serviceRequestRepository;
    }

    private readonly IMapper _mapper;

    private readonly IServiceRequestRepository _serviceRequestRepository;

    public async Task<List<ServiceRequestPreviewDto>> GetGroupRequestsPreview(int groupId)
    {
        var dbServiceRequests = await _serviceRequestRepository.GetByGroup(groupId);

        var serviceRequests = _mapper.Map<List<ServiceRequestPreviewDto>>(dbServiceRequests);

        return serviceRequests;
    }

    public async Task<ServiceRequestDto> GetServiceRequestOverview(int requestId)
    {
        var dbServiceRequest = await _serviceRequestRepository.GetByIdAsync(requestId);

        var serviceRequest = _mapper.Map<ServiceRequestDto>(dbServiceRequest);

        if (serviceRequest is null)
        {
            throw new ServiceRequestNotFoundException(requestId);
        }

        return serviceRequest;
    }

    public async Task CreateRequest(CreateRequestDto serviceRequest)
    {
        var dbRequest = _mapper.Map<ServiceRequest>(serviceRequest);

        await _serviceRequestRepository.CreateAsync(dbRequest);
    }

    public async Task UpdateRequest(UpdateRequestDto serviceRequest)
    {
        var dbRequest = _mapper.Map<ServiceRequest>(serviceRequest);

        await _serviceRequestRepository.UpdateAsync(dbRequest);
    }
}
