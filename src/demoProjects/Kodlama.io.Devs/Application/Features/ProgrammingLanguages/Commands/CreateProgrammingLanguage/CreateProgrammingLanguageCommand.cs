using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
{
    public string Name { get; set; }

    public class
        CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
    {
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;

        public CreateProgrammingLanguageCommandHandler(ProgrammingLanguageBusinessRules programmingLanguageBusinessRules,
            IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }

        public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            await _programmingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

            ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);
            CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

            return createdProgrammingLanguageDto;
        }
    }
}