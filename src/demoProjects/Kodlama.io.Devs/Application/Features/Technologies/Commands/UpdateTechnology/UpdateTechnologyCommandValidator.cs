using Application.Features.Technologies.Commands.CreateTechnology;
using FluentValidation;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
    }
}