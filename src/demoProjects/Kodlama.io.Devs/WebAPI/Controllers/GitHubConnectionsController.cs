using Application.Features.GitHubConnections.Commands.CreateGitHubConnection;
using Application.Features.GitHubConnections.Queries.GetListGitHubConnectionList;
using Core.Application.Requests;
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

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListGitHubConnectionListQuery getListGitHubConnectionListQuery = new() { PageRequest = pageRequest };
        var result = await Mediator.Send(getListGitHubConnectionListQuery);
        return Ok(result);
    }
}