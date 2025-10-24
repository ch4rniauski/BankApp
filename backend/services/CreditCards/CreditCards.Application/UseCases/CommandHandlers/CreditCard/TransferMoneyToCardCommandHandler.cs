using ch4rniauski.BankApp.CreditCards.Application.Checkers;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCard;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.CommandHandlers.CreditCard;

internal sealed class TransferMoneyToCardCommandHandler : IRequestHandler<TransferMoneyToCardCommand, Result<TransferMoneyResponseDto>>
{
    private readonly ICreditCardRepository _creditCardRepository;

    public TransferMoneyToCardCommandHandler(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;
    }

    public async Task<Result<TransferMoneyResponseDto>> Handle(TransferMoneyToCardCommand request, CancellationToken cancellationToken)
    {
        var receiverCard = await _creditCardRepository.GetByCardNumberAsync(
            request.Request.ReceiverCardNumber,
            cancellationToken);

        var cardCheckResult = CreditCardChecker.CheckCreditCard(
            receiverCard,
            request.Request.ReceiverCardNumber,
            request.Request.ReceiverId);

        if (cardCheckResult is not null)
        {
            return cardCheckResult;
        }
        
        var senderCard = await _creditCardRepository.GetByCardNumberAsync(
            request.Request.SenderCardNumber,
            cancellationToken);
        
        cardCheckResult = CreditCardChecker.CheckCreditCard(
            senderCard,
            request.Request.SenderCardNumber,
            request.Request.SenderId);
        
        if (cardCheckResult is not null)
        {
            return cardCheckResult;
        }
        
        if (senderCard!.Balance < request.Request.Amount)
        {
            return Result<TransferMoneyResponseDto>
                .Failure(Error.FundsLack(
                    "Not enough money"
                    ));
        }
        
        var isSuccess = await _creditCardRepository.TransferMoneyAsync(
            senderCardNumber: request.Request.SenderCardNumber,
            receiverCardNumber: request.Request.ReceiverCardNumber,
            amount: request.Request.Amount,
            cancellationToken: cancellationToken);

        if (!isSuccess)
        {
            
        }
    }
}