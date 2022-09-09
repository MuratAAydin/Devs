using Application.Features.GitHubConnections.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubConnections.Commands.UpdateGitHubConnection;

public class UpdateGitHubConnectionCommand : IRequest<UpdatedGitHubConnectDto>
{
    public int UserId { get; set; }
    public string Name { get; set; }

    public class
        UpdateGitHubConnectionCommandHandler : IRequestHandler<UpdateGitHubConnectionCommand, UpdatedGitHubConnectDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubConnectionRepository _gitHubConnectionRepository;

        public UpdateGitHubConnectionCommandHandler(IMapper mapper,
            IGitHubConnectionRepository gitHubConnectionRepository)
        {
            _mapper = mapper;
            _gitHubConnectionRepository = gitHubConnectionRepository;
        }

        public async Task<UpdatedGitHubConnectDto> Handle(UpdateGitHubConnectionCommand request,
            CancellationToken cancellationToken)
        {
            var gitHubConnection = await _gitHubConnectionRepository.GetAsync(e => e.UserId == request.UserId);
            var mappedGitHubConnection = _mapper.Map(request, gitHubConnection);
            var createdGitHubConnection = await _gitHubConnectionRepository.UpdateAsync(mappedGitHubConnection!);
            var createdGitHubConnectionDto = _mapper.Map<UpdatedGitHubConnectDto>(createdGitHubConnection);
            return createdGitHubConnectionDto;
        }
    }
}