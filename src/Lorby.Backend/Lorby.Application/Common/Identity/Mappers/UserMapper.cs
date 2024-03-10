using AutoMapper;
using Lorby.Application.Common.Identity.Models;
using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Identity.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SignUpDetails, User>();
    }
}