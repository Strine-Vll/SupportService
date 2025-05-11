using Application.Dtos.UserDtos;
using Domain.Entities;

namespace Application.Dtos.ServiceRequestDtos;

public class EditServiceRequestDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }

    public UserPreviewDto? Appointed { get; set; }

    public Group? Group { get; set; }
}
