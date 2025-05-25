using Application.Dtos.AuditLogDtos;
using AutoMapper;
using Infrastructure;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SupportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public AuditLogController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetRequestLogs(int requestId)
        {
            var dbResult = await _dbContext.AuditLogs
                .Where(l => l.EntityId == requestId.ToString())
                .ToListAsync();

            var userIds = dbResult
                .Where(l => !string.IsNullOrEmpty(l.ChangedBy))
                .Select(l => int.Parse(l.ChangedBy!))
                .ToList();

            var names = await _dbContext.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => u.Name)
                .ToListAsync();

            var dtos = _mapper.Map<List<AuditLogDto>>(dbResult);

            for (int i = 0; i < names.Count; i++)
            {
                dtos[i].ChangedBy = names[i];
            }

            return Ok(dtos);
        }
    }
}
