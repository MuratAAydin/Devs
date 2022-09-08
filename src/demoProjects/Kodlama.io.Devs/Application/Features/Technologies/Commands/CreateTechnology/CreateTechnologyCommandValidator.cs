using FluentValidation;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(c => c.ProgrammingLanguageId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}