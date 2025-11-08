using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.Extensions;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ch4rniauski.BankApp.CreditCards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreditCardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateCreditCardResponseDto>> CreateCreditCard(
        [FromBody] CreateCreditCardRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCreditCardCommand(request);
        
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            onSuccess: Ok,
            onFailure: err => Problem(
                detail: err.Message,
                statusCode: err.StatusCode));
    }

    [HttpGet("{clientId:guid}")]
    public async Task<ActionResult<IList<GetCreditCardResponseDto>>> GetCreditCardsByClientId(Guid clientId, CancellationToken cancellationToken)
    {
        var query = new GetCreditCardsByClientIdQuery(clientId);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
