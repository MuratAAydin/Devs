using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageCommandValidator : AbstractValidator<DeleteProgrammingLanguageCommand>
{
    public DeleteProgrammingLanguageCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}