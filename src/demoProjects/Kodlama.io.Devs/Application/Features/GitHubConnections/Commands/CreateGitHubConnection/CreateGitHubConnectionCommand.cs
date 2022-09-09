using Application.Features.GitHubConnections.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubConnections.Commands.CreateGitHubConnection;

public class CreateGitHubConnectionCommand : IRequest<CreatedGitHubConnectDto>
{
    public int UserId { get; set; }
    public string Name { get; set; }

    public class
        CreateGitHubConnectionCommandHandler : IRequestHandler<CreateGitHubConnectionCommand, CreatedGitHubConnectDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubConnectionRepository _gitHubConnectionRepository;

        public CreateGitHubConnectionCommandHandler(IMapper mapper,
            IGitHubConnectionRepository gitHubConnectionRepository)
        {
            _mapper = mapper;
            _gitHubConnectionRepository = gitHubConnectionRepository;
        }

        public async Task<CreatedGitHubConnectDto> Handle(CreateGitHubConnectionCommand request,
            CancellationToken cancellationToken)
        {
            var mappedGitHubConnection = _mapper.Map<GitHubConnection>(request);
            var createdGitHubConnection = await _gitHubConnectionRepository.AddAsync(mappedGitHubConnection);
            var createdGitHubConnectionDto = _mapper.Map<CreatedGitHubConnectDto>(createdGitHubConnection);
            return createdGitHubConnectionDto;
        }
    }
}