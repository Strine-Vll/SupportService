using Application.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface ICommentService
{
    Task<List<CommentDto>> GetRequestComments(int requestId);

    Task CreateComment(CreateCommentDto commentDto);
}
