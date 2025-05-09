using Application.Dtos.AttachmentDtos;
using Application.Dtos.CommentDtos;
using Application.Dtos.GroupDtos;
using Application.Dtos.ServiceRequestDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Settings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<RegisterDto, User>();

        CreateMap<AuthenticationRequest, User>().ReverseMap();

        CreateMap<User, UserPreviewDto>().ReverseMap();

        CreateMap<Group, GroupDto>().ReverseMap();

        CreateMap<ServiceRequest, ServiceRequestDto>()
            .ForMember(x => x.CreatedBy, y => y.MapFrom(x => x.CreatedBy.Name))
            .ForMember(x => x.Status, y => y.MapFrom(x => x.Status.StatusName));

        CreateMap<ServiceRequest, ServiceRequestPreviewDto>()
            .ForMember(x => x.Status, y => y.MapFrom(x => x.Status.StatusName));

        CreateMap<CreateRequestDto, ServiceRequest>();

        CreateMap<EditServiceRequestDto, ServiceRequest>().ReverseMap();

        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.Name, y => y.MapFrom(x => x.CreatedBy != null ? x.CreatedBy.Name : null))
            .ForMember(x => x.Email, y => y.MapFrom(x => x.CreatedBy != null ? x.CreatedBy.Email : null));


        CreateMap<CreateCommentDto, Comment>();

        CreateMap<Attachment, AttachmentDto>()
            .ForMember(x => x.Url,
                       y => y.MapFrom(src => $"https://localhost:7239/api/Attachment?attachmentId={src.Id}"));
    }
}
