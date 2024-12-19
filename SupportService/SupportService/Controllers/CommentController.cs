using Application.Abstractions;
using Application.Dtos.CommentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("RequestComments")]
    public async Task<ActionResult> GetRequestComments(int requestId)
    {
        var result = await _commentService.GetRequestComments(requestId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateComment(CreateCommentDto commentDto)
    {
        await _commentService.CreateComment(commentDto);

        return Ok();
    }
}
