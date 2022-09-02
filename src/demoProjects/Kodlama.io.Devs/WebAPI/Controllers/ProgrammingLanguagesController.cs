using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammingLanguagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(createProgrammingLanguageCommand);
        return Created("", result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(deleteProgrammingLanguageCommand);
        return Created("", result);
    }
}