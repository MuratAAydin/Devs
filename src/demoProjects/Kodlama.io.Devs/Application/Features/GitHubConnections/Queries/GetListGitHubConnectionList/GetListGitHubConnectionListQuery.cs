using Application.Features.GitHubConnections.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.GitHubConnections.Queries.GetListGitHubConnectionList;

public class GetListGitHubConnectionListQuery : IRequest<GitHubConnectionListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListGitHubConnectionListQueryHandler : IRequestHandler<GetListGitHubConnectionListQuery,
            GitHubConnectionListModel>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubConnectionRepository _gitHubConnectionRepository;

        public GetListGitHubConnectionListQueryHandler(IMapper mapper,
            IGitHubConnectionRepository gitHubConnectionRepository)
        {
            _mapper = mapper;
            _gitHubConnectionRepository = gitHubConnectionRepository;
        }

        public async Task<GitHubConnectionListModel> Handle(GetListGitHubConnectionListQuery request,
            CancellationToken cancellationToken)
        {
            var gitHubConnection = await _gitHubConnectionRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedGitHubConnectionListModel = _mapper.Map<GitHubConnectionListModel>(gitHubConnection);

            return mappedGitHubConnectionListModel;
        }
    }
}