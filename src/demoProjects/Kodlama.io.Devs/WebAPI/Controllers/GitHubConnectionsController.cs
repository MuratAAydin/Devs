using Application.Features.GitHubConnections.Commands.CreateGitHubConnection;
using Application.Features.GitHubConnections.Commands.DeleteGitHubConnection;
using Application.Features.GitHubConnections.Commands.UpdateGitHubConnection;
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

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteGitHubConnectionCommand deleteGitHubConnectionCommand)
    {
        var result = await Mediator.Send(deleteGitHubConnectionCommand);
        return Created("", result);
    }
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateGitHubConnectionCommand updateGitHubConnectionCommand)
    {
        var result = await Mediator.Send(updateGitHubConnectionCommand);
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