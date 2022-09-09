using Application.Features.GitHubConnections.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubConnections.Commands.DeleteGitHubConnection;

public class DeleteGitHubConnectionCommand : IRequest<DeletedGitHubConnectDto>
{
    public int UserId { get; set; }

    public class
        DeleteGitHubConnectionCommandHandler : IRequestHandler<DeleteGitHubConnectionCommand, DeletedGitHubConnectDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubConnectionRepository _gitHubConnectionRepository;

        public DeleteGitHubConnectionCommandHandler(IMapper mapper,
            IGitHubConnectionRepository gitHubConnectionRepository)
        {
            _mapper = mapper;
            _gitHubConnectionRepository = gitHubConnectionRepository;
        }

        public async Task<DeletedGitHubConnectDto> Handle(DeleteGitHubConnectionCommand request,
            CancellationToken cancellationToken)
        {
            var gitHubConnection = await _gitHubConnectionRepository.GetAsync(e => e.UserId == request.UserId);
            var mappedGitHubConnection = _mapper.Map<GitHubConnection>(gitHubConnection);
            var createdGitHubConnection = await _gitHubConnectionRepository.DeleteAsync(mappedGitHubConnection);
            var createdGitHubConnectionDto = _mapper.Map<DeletedGitHubConnectDto>(createdGitHubConnection);
            return createdGitHubConnectionDto;
        }
    }
}