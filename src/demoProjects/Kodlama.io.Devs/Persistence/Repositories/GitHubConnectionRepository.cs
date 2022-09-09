using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GitHubConnectionRepository : EfRepositoryBase<GitHubConnection, BaseDbContext>,
    IGitHubConnectionRepository
{
    public GitHubConnectionRepository(BaseDbContext context) : base(context)
    {
    }
}