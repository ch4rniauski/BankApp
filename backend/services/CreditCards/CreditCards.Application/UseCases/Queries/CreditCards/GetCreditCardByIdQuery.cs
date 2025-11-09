using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;

public sealed record GetCreditCardByIdQuery(Guid CardId) : IRequest<Result<GetCreditCardResponseDto>>;
