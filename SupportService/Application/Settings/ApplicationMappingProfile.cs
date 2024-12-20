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

        CreateMap<User, UserPreviewDto>();

        CreateMap<Group, GroupDto>().ReverseMap();

        CreateMap<ServiceRequest, ServiceRequestDto>()
            .ForMember(x => x.CreatedBy, y => y.MapFrom(x => x.CreatedBy.Name));

        CreateMap<ServiceRequest, ServiceRequestPreviewDto>()
            .ForMember(x => x.Status, y => y.MapFrom(x => x.Status.StatusName));

        CreateMap<CreateRequestDto, ServiceRequest>();

        CreateMap<UpdateRequestDto, ServiceRequest>();

        CreateMap<Comment, CommentDto>();

        CreateMap<CreateCommentDto, Comment>();
    }
}
