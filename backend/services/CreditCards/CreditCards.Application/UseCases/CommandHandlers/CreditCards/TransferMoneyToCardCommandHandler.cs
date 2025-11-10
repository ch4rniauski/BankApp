using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Checkers;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.CommandHandlers.CreditCards;

internal sealed class TransferMoneyToCardCommandHandler : IRequestHandler<TransferMoneyToCardCommand, Result<TransferMoneyResponseDto>>
{
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IMapper _mapper;

    public TransferMoneyToCardCommandHandler(
        ICreditCardRepository creditCardRepository,
        IMapper mapper)
    {
        _creditCardRepository = creditCardRepository;
        _mapper = mapper;
    }

    public async Task<Result<TransferMoneyResponseDto>> Handle(TransferMoneyToCardCommand request, CancellationToken cancellationToken)
    {
        var receiverCard = await _creditCardRepository.GetCardByNumberAsync(
            request.Request.ReceiverCardNumber,
            cancellationToken);

        var cardCheckResult = CreditCardChecker.CheckCreditCard(
            receiverCard,
            request.Request.ReceiverCardNumber);

        if (cardCheckResult is not null)
        {
            return cardCheckResult;
        }
        
        var senderCard = await _creditCardRepository.GetCardByNumberAsync(
            request.Request.SenderCardNumber,
            cancellationToken);
        
        cardCheckResult = CreditCardChecker.CheckCreditCard(
            senderCard,
            request.Request.SenderCardNumber);
        
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
            request.Request.SenderCardNumber,
            request.Request.ReceiverCardNumber,
            request.Request.Amount,
            cancellationToken);

        if (!isSuccess)
        {
            return Result<TransferMoneyResponseDto>
                .Failure(Error.InternalError(
                    "Error occurred while transfering money"
                    ));
        }
        
        var response = _mapper.Map<TransferMoneyResponseDto>(request.Request);

        response.ReceiverId = receiverCard!.CardHolderId;
        response.SenderId = senderCard.CardHolderId;

        return Result<TransferMoneyResponseDto>.Success(response);
    }
}