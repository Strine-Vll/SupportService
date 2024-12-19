using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ServiceRequestDtos;

public class CreateRequestDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int GroupId { get; set; }

    public int CreatedById { get; set; }
}
