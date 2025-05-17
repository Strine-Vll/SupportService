using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ServiceRequestStatsDtos;

public class StatDto
{
    public int Id { get; set; }

    public double SatisfactionIndex { get; set; }

    public int ReescalateAmount { get; set; }

    public TimeSpan ReactionTime { get; set; }

    public TimeSpan ResolutionTime { get; set; }
}
