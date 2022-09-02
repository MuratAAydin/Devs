using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
    {
        var result = await _programmingLanguageRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException("Programming language name exists.");
    }

    public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
    {
        if (programmingLanguage == null) throw new BusinessException("Requested programming language does not exists.");
    }
}