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

    public async Task<List<ServiceRequestPreviewDto>> GetUserRequestsPreview(int userId)
    {
        var dbServiceRequests = await _serviceRequestRepository.GetPreviewByUser(userId);

        var serviceRequests = _mapper.Map<List<ServiceRequestPreviewDto>>(dbServiceRequests);

        return serviceRequests;
    }

    public async Task<List<ServiceRequestPreviewDto>> GetUnallocatedRequests()
    {
        var dbServiceRequests = await _serviceRequestRepository.GetUnallocatedRequests();

        var serviceRequests = _mapper.Map<List<ServiceRequestPreviewDto>>(dbServiceRequests);

        return serviceRequests;
    }

    public async Task<ServiceRequestDto> GetServiceRequestOverview(int requestId)
    {
        var dbServiceRequest = await _serviceRequestRepository.GetOverviewById(requestId);

        var serviceRequest = _mapper.Map<ServiceRequestDto>(dbServiceRequest);

        if (serviceRequest is null)
        {
            throw new ServiceRequestNotFoundException(requestId);
        }

        return serviceRequest;
    }

    public async Task<EditServiceRequestDto> GetEditRequest(int requestId)
    {
        var dbServiceRequest = await _serviceRequestRepository.GetOverviewById(requestId);

        var serviceRequest = _mapper.Map<EditServiceRequestDto>(dbServiceRequest);

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

    public async Task UpdateRequest(EditServiceRequestDto serviceRequest)
    {
        var dbRequest = await _serviceRequestRepository.GetRequestForUpdate(serviceRequest.Id);

        UpdateRequestFields(dbRequest, serviceRequest);

        await _serviceRequestRepository.UpdateAsync(dbRequest);
    }

    public async Task DeleteRequest(int requestId)
    {
        await _serviceRequestRepository.DeleteByIdAsync(requestId);
    }

    public async Task CloseRequest(int requestId)
    {
        var result = await _serviceRequestRepository.GetByIdAsync(requestId);

        result.StatusId = 6;

        await _serviceRequestRepository.UpdateAsync(result);
    }

    public async Task ReescalateRequest(int requestId)
    {
        var result = await _serviceRequestRepository.GetByIdAsync(requestId);

        result.StatusId = 5;
        result.Status = new Status
        {
            Id = 5,
            StatusName = "На доработку"
        };

        await _serviceRequestRepository.UpdateAsync(result);
    }

    private void UpdateRequestFields(ServiceRequest dbRequest, EditServiceRequestDto updateRequest)
    {
        dbRequest.Title = updateRequest.Title;
        dbRequest.Description = updateRequest.Description;
        dbRequest.UpdatedDate = DateTime.UtcNow;
        dbRequest.AppointedId = updateRequest.Appointed?.Id;
        dbRequest.Status = updateRequest.Status;
        dbRequest.StatusId = updateRequest.Status.Id;
        dbRequest.GroupId = updateRequest.Group?.Id;
    }
}
