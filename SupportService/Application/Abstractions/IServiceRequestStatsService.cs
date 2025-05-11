using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IServiceRequestStatsService
{
    Task CloseServiceRequest(int requestId, double satisfactionIndex);
}
