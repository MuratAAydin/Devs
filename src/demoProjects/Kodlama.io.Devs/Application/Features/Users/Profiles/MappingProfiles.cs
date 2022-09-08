using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Security.Entities;


namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreatedUserDto>().ReverseMap();
        CreateMap<User, CreateUserCommand>().ReverseMap();
    }
}