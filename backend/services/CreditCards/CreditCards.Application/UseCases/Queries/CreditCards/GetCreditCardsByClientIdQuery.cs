using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;

public sealed record GetCreditCardsByClientIdQuery(Guid ClientId) : IRequest<IList<GetCreditCardResponseDto>>;
