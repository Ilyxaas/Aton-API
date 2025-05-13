using AutoMapper;
using DataAccess.Models;
using Shared.ApiForm;

namespace AtonWebAPI.MappingProfile;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(user => user.Active, opt =>
                opt.MapFrom(src => src.RevokedOn == null)); 
    }
}