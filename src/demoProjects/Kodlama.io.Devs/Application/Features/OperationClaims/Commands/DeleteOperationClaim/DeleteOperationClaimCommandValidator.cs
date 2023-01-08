using FluentValidation;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommandValidator : AbstractValidator<DeleteOperationClaimCommand>
{
    public DeleteOperationClaimCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}