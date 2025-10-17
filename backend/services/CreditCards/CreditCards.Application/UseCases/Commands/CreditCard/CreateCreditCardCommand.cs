using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCard;

public sealed record CreateCreditCardCommand(CreateCreditCardRequestDto Request) : IRequest<Result<CreateCreditCardResponseDto>>;
