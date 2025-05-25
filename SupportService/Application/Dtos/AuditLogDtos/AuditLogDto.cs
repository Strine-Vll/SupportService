using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AuditLogDtos;

public class AuditLogDto
{
    public string EntityId { get; set; }

    public string PropertyName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime ChangedAt { get; set; }

    public string? ChangedBy { get; set; }
}
