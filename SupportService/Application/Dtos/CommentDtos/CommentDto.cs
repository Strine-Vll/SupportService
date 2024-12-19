using Application.Dtos.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CommentDtos;

public class CommentDto
{
    public int Id { get; set; }

    public string Message { get; set; }

    public DateTime CreatedAt { get; set; }

    #region Navigation Properties

    public UserPreviewDto CreatedBy { get; set; }

    #endregion
}
