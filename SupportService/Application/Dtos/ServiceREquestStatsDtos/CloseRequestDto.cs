using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ServiceREquestStatsDtos;

public class CloseRequestDto
{
    public int RequestId { get; set; }

    public double SatisfactionIndex { get; set; }
}
