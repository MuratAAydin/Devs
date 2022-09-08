using Application.Features.Technologies.Commands.CreateTechnology;
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
}