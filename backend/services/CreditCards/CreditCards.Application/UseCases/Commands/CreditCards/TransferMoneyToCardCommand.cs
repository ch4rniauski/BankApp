using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCards;

public sealed record TransferMoneyToCardCommand(TransferMoneyRequestDto Request) : IRequest<Result<TransferMoneyResponseDto>>;
