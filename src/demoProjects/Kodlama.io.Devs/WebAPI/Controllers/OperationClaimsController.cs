using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
    {
        var result = await Mediator.Send(createOperationClaimCommand);
        return Created("", result);
    }
}