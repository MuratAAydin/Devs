using Application.Features.GitHubConnections.Commands.CreateGitHubConnection;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GitHubConnectionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateGitHubConnectionCommand createGitHubConnectionCommand)
    {
        var result = await Mediator.Send(createGitHubConnectionCommand);
        return Created("", result);
    }

}