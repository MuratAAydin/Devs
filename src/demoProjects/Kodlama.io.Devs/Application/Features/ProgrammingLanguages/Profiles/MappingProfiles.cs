using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
        CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap();
        // CreateMap<Brand, CreatedBrandDto>().ReverseMap();
        // CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        // CreateMap<IPaginate<Brand>, BrandListModel>().ReverseMap();
    }
}