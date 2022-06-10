using ApiTreino.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiTreino.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser<int>, UserDto>();
            CreateMap<UserDto, IdentityUser<int>>();
        }
    }
}
