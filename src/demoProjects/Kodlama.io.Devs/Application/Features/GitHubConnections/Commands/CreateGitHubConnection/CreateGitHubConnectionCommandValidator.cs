using FluentValidation;

namespace Application.Features.GitHubConnections.Commands.CreateGitHubConnection;

public class CreateGitHubConnectionCommandValidator : AbstractValidator<CreateGitHubConnectionCommand>
{
    public CreateGitHubConnectionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}