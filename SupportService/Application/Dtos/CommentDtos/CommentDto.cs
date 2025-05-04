using Application.Dtos.AttachmentDtos;

namespace Application.Dtos.CommentDtos;

public class CommentDto
{
    public int Id { get; set; }

    public string Message { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<AttachmentDto> Attachments { get; set; }

    public DateTime CreatedAt { get; set; }
}
