using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Responses.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.Extensions;
using ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ch4rniauski.BankApp.MoneyTransfer.Api.Controllers;

[ApiController]
[Route("api/money")]
public class MoneyTransferController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoneyTransferController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("transfer")]
    public async Task<ActionResult<TransferMoneyResponseDto>> TransferMoney(
        [FromBody] TransferMoneyRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new TransferMoneyToCardCommand(request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return result.Match(
            onSuccess: Ok,
            onFailure: err => Problem(
                detail: err.Message,
                statusCode: err.StatusCode));
    }
}
