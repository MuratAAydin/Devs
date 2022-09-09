using FluentValidation;

namespace Application.Features.GitHubConnections.Commands.UpdateGitHubConnection;

public class UpdateGitHubConnectionCommandValidator : AbstractValidator<UpdateGitHubConnectionCommand>
{
    public UpdateGitHubConnectionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}