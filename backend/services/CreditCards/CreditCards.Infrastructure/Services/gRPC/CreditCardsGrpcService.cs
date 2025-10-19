using ch4rniauski.BankApp.CreditCards.Grpc;
using Grpc.Core;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Services.gRPC;

public sealed class CreditCardsGrpcService : CreditCardsGrpc.CreditCardsGrpcBase
{
    private readonly IMediator _mediator;

    public CreditCardsGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override Task<TransferMoneyToCardResponse> TransferMoneyToCard(TransferMoneyToCardRequest request, ServerCallContext context)
    {
        
    }
}
