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

        CreateMap<Group, GroupDto>().ReverseMap();

        CreateMap<ServiceRequest, ServiceRequestDto>()
            .ForMember(x => x.CreatedBy, y => y.MapFrom(x => x.CreatedBy.Name));
    }
}
