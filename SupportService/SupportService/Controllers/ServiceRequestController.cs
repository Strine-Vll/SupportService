using Application.Abstractions;
using Application.Dtos.ServiceRequestDtos;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ServiceRequestController : ControllerBase
{
    private readonly IServiceRequestService _serviceRequestService;

    public ServiceRequestController(IServiceRequestService serviceRequestService)
    {
        _serviceRequestService = serviceRequestService;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<ActionResult> CreateServiceRequest(CreateRequestDto serviceRequest)
    {
        await _serviceRequestService.CreateRequest(serviceRequest);

        return Ok();
    }

    [HttpPost("UpdateRequest")]
    public async Task<ActionResult> UpdateServiceRequest(UpdateRequestDto serviceRequest)
    {
        await _serviceRequestService.UpdateRequest(serviceRequest);

        return Ok();
    }
}
