using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ServiceRequestDtos;

public class UpdateRequestDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? GroupId { get; set; }

    public int? AppointedId { get; set; }

    public Status Status { get; set; }
}
