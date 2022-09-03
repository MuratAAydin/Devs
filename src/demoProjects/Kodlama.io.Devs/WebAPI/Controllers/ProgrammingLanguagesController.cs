using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammingLanguagesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
    {
        var programmingLanguageGetByIdDto = await Mediator.Send(getByIdProgrammingLanguageQuery);
        return Ok(programmingLanguageGetByIdDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
        var result = await Mediator.Send(getListProgrammingLanguageQuery);
        return Ok(result);
    }

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

    [HttpPut]
    public async Task<IActionResult> Put(
        [FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(updateProgrammingLanguageCommand);
        return Created("", result);
    }
}