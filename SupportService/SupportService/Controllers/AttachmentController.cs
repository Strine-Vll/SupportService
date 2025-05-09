using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;

    public AttachmentController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAttachment(int attachmentId)
    {
        var attachment = await _attachmentService.GetAttachmentByIdAsync(attachmentId);

        var contentType = "application/octet-stream";

        var fileName = attachment.Name;

        return File(attachment.Content, contentType, fileName);
    }
}
