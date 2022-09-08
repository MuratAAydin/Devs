using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
        CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
    }
}