using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ServiceRequestStats
{
    public int Id { get; set; }

    public double SatisfactionIndex { get; set; }

    public int ReescalateAmount { get; set; } = 0;

    public TimeSpan ReactionTime { get; set; }

    public TimeSpan ResolutionTime { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public int? ServiceRequestId { get; set; }

    public ServiceRequest? ServiceRequest { get; set; }

    public int? UserId { get; set; }

    public User? User { get; set; }
}
