using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ch4rniauski.BankApp.Authentication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterClientResponseDto>> RegisterClient(
        [FromBody]RegisterClientRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterClientCommand(request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}
