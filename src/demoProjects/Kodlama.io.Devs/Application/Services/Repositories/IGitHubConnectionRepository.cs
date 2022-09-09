using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IGitHubConnectionRepository : IAsyncRepository<GitHubConnection>, IRepository<GitHubConnection>
{
}