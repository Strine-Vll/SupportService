using Application.Abstractions;
using Application.Dtos.ServiceRequestStatsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestStatController : ControllerBase
    {
        private readonly IServiceRequestStatsService _serviceRequestStatsService;

        public ServiceRequestStatController(IServiceRequestStatsService serviceRequestStatsService)
        {
            _serviceRequestStatsService = serviceRequestStatsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetServiceRequestStat(int id)
        {
            var result = await _serviceRequestStatsService.GetRequestStat(id);

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetFilterRequestStat([FromQuery] FilterStatDto filterStat)
        {
            var result = await _serviceRequestStatsService.FilterStat(filterStat);

            return Ok(result);
        }
    }
}
