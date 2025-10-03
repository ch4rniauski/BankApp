using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ch4rniauski.BankApp.Authentication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
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

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<DeleteClientResponseDto>> DeleteClient(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand(id);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateClientResponseDto>> UpdateClient(
        Guid id,
        [FromBody]UpdateClientRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClientCommand(id, request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginClientResponseDto>> LoginClient(
        [FromBody] LoginClientRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new LoginClientCommand(request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}
