using Application.Features.GitHubConnections.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.GitHubConnections.Models;

public class GitHubConnectionListModel : BasePageableModel
{
    public IList<GitHubConnectionListDto> Items { get; set; }
}