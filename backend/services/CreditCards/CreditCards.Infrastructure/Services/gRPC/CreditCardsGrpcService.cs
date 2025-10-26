using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.Extensions;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCard;
using ch4rniauski.BankApp.CreditCards.Grpc;
using Grpc.Core;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Services.gRPC;

public sealed class CreditCardsGrpcService : CreditCardsGrpc.CreditCardsGrpcBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreditCardsGrpcService(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<TransferMoneyToCardResponse> TransferMoneyToCard(TransferMoneyToCardRequest request, ServerCallContext context)
    {
        var requestDto = _mapper.Map<TransferMoneyRequestDto>(request);
        
        var command = new TransferMoneyToCardCommand(requestDto);
        
        var result = await _mediator.Send(command, context.CancellationToken);

        return result.Match(
            onSuccess: src => _mapper.Map<TransferMoneyToCardResponse>(src),
            onFailure: err => _mapper.Map<TransferMoneyToCardResponse>(err));
    }
}
