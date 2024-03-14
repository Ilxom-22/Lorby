using AutoMapper;
using Lorby.Application.Common.Identity.Models;
using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Identity.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SignUpDetails, User>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
    }
}