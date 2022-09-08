using Application.Features.Technologies.Commands.CreateTechnology;
using FluentValidation;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public DeleteTechnologyCommandValidator()
    {
    }
}