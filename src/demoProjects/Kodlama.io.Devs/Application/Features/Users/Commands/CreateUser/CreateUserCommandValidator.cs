using Application.Features.Technologies.Commands.CreateTechnology;
using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.ProgrammingLanguageId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}