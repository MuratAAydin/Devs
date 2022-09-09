using Application.Features.GitHubConnections.Commands.CreateGitHubConnection;
using Application.Features.GitHubConnections.Commands.DeleteGitHubConnection;
using Application.Features.GitHubConnections.Commands.UpdateGitHubConnection;
using Application.Features.GitHubConnections.Dtos;
using Application.Features.GitHubConnections.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubConnections.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GitHubConnection, GitHubConnectionListDto>().ReverseMap();
        CreateMap<GitHubConnection, CreatedGitHubConnectDto>().ReverseMap();
        CreateMap<GitHubConnection, DeletedGitHubConnectDto>().ReverseMap();
        CreateMap<GitHubConnection, UpdatedGitHubConnectDto>().ReverseMap();
        CreateMap<GitHubConnection, CreateGitHubConnectionCommand>().ReverseMap();
        CreateMap<GitHubConnection, DeleteGitHubConnectionCommand>().ReverseMap();
        CreateMap<GitHubConnection, UpdateGitHubConnectionCommand>().ReverseMap();
        CreateMap<GitHubConnection, Task<GitHubConnection>>().ReverseMap();
        CreateMap<IPaginate<GitHubConnection>, GitHubConnectionListModel>().ReverseMap();
    }
}