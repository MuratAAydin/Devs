using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
    {
        var result = await Mediator.Send(createTechnologyCommand);
        return Created("", result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand deleteTechnologyCommand)
    {
        var result = await Mediator.Send(deleteTechnologyCommand);
        return Created("", result);
    }
}