using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CommentDtos;

public class CreateCommentDto
{
    public string Message { get; set; }

    public int ServiceRequestId { get; set; }

    public int CreatedById { get; set; }
}
