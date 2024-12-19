using Application.Abstractions;
using Application.Dtos.CommentDtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    private IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<List<CommentDto>> GetRequestComments(int requestId)
    {
        var dbComments = await _commentRepository.GetCommentsByServiceRequest(requestId);

        var comments = _mapper.Map<List<CommentDto>>(dbComments);

        return comments;
    }

    public async Task CreateComment(CreateCommentDto commentDto)
    {
        var comment = _mapper.Map<Comment>(commentDto);

        await _commentRepository.CreateAsync(comment);
    }
}
