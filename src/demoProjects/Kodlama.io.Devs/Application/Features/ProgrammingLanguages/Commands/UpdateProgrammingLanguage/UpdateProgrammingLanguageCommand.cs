using System.Diagnostics;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdateProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class
        UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand,
            UpdateProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public UpdateProgrammingLanguageCommandHandler(
            ProgrammingLanguageBusinessRules programmingLanguageBusinessRules,
            IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(e => e.Id == request.Id);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);
            await _programmingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

            ProgrammingLanguage? mappedProgrammingLanguage = _mapper.Map(request, programmingLanguage);
            ProgrammingLanguage updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(mappedProgrammingLanguage);
            UpdateProgrammingLanguageDto updateProgrammingLanguageDto = _mapper.Map<UpdateProgrammingLanguageDto>(updatedProgrammingLanguage);

            return updateProgrammingLanguageDto;
        }
    }
}