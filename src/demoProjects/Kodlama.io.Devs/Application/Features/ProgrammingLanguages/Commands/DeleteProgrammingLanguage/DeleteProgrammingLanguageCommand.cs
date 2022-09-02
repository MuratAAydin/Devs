using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
{
    public string Name { get; set; }

    public class
        DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand,
            DeletedProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public DeleteProgrammingLanguageCommandHandler(
            ProgrammingLanguageBusinessRules programmingLanguageBusinessRules,
            IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            var programmingLanguage =
                await _programmingLanguageRepository.GetAsync(e => e.Name == request.Name);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            var deletedProgrammingLanguage =
                await _programmingLanguageRepository.DeleteAsync(programmingLanguage);
            var deletedProgrammingLanguageDto =
                _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

            return deletedProgrammingLanguageDto;
        }
    }
}