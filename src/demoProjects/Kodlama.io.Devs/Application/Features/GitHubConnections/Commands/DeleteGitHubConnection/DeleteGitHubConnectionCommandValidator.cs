using FluentValidation;

namespace Application.Features.GitHubConnections.Commands.DeleteGitHubConnection;

public class DeleteGitHubConnectionCommandValidator : AbstractValidator<DeleteGitHubConnectionCommand>
{
    public DeleteGitHubConnectionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}