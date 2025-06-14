﻿using Application.Abstractions;
using Application.Dtos.ServiceRequestDtos;
using Application.Dtos.ServiceREquestStatsDtos;
using Application.Exceptions;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ServiceRequestController : ControllerBase
{
    private readonly IServiceRequestService _serviceRequestService;

    private readonly IServiceRequestStatsService _serviceRequestStatsService;

    public ServiceRequestController(IServiceRequestService serviceRequestService, IServiceRequestStatsService serviceRequestStatsService)
    {
        _serviceRequestService = serviceRequestService;
        _serviceRequestStatsService = serviceRequestStatsService;
    }

    [HttpGet("RequestsPreview")]
    public async Task<ActionResult> GetGroupRequestsPreview(int groupId)
    {
        var result = await _serviceRequestService.GetGroupRequestsPreview(groupId);

        return Ok(result);
    }

    [HttpGet("GetOverview")]
    public async Task<ActionResult> GetServiceRequestOverview(int requestId)
    {
        try
        {
            var result = await _serviceRequestService.GetServiceRequestOverview(requestId);

            return Ok(result);
        }
        catch (ServiceRequestNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetRequestsForProcessing")]
    public async Task<ActionResult> GetRequestsForProcessing(int userId)
    {
        try
        {
            var result = await _serviceRequestService.GetRequestsForProcessing(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetUserRequests")]
    public async Task<ActionResult> GetUserRequestsPreview(int userId)
    {
        try
        {
            var result = await _serviceRequestService.GetUserRequestsPreview(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetUnallocatedRequests")]
    public async Task<ActionResult> GetUnallocatedRequests()
    {
        try
        {
            var result = await _serviceRequestService.GetUnallocatedRequests();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetEditRequest")]
    public async Task<ActionResult> GetEditRequest(int requestId)
    {
        try
        {
            var result = await _serviceRequestService.GetEditRequest(requestId);

            return Ok(result);
        }
        catch (ServiceRequestNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateServiceRequest(CreateRequestDto serviceRequest)
    {
        await _serviceRequestService.CreateRequest(serviceRequest);

        return Ok();
    }

    [HttpPut("UpdateRequest")]
    public async Task<ActionResult> UpdateServiceRequest(EditServiceRequestDto serviceRequest)
    {
        await _serviceRequestService.UpdateRequest(serviceRequest);

        if (serviceRequest.Status.Id == 6)
        {
            await _serviceRequestStatsService.CloseServiceRequest(serviceRequest.Id, 0);
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteServiceRequest(int id)
    {
        await _serviceRequestService.DeleteRequest(id);

        return Ok();
    }

    [HttpPost("CloseRequest")]
    public async Task<ActionResult> CloseServiceRequest(CloseRequestDto closeRequestDto)
    {
        await _serviceRequestService.CloseRequest(closeRequestDto.RequestId);
        await _serviceRequestStatsService.CloseServiceRequest(closeRequestDto.RequestId, closeRequestDto.SatisfactionIndex);

        return Ok();
    }

    [HttpPost("ReescalateRequest")]
    public async Task<ActionResult> ReescalateServiceRequest(int id)
    {
        await _serviceRequestService.ReescalateRequest(id);
        await _serviceRequestStatsService.ReescalateRequest(id);

        return Ok();
    }
}
