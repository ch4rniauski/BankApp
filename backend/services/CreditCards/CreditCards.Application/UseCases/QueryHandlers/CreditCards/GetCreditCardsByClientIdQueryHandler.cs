using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.QueryHandlers.CreditCards;

internal sealed class GetCreditCardsByClientIdQueryHandler : IRequestHandler<GetCreditCardsByClientIdQuery, IList<GetCreditCardResponseDto>>
{
    private readonly ICreditCardRepository _creditCardRepository;

    public GetCreditCardsByClientIdQueryHandler(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;
    }

    public async Task<IList<GetCreditCardResponseDto>> Handle(GetCreditCardsByClientIdQuery request, CancellationToken cancellationToken)
        => await _creditCardRepository.GetCardsByClientId<GetCreditCardResponseDto>(request.ClientId, cancellationToken);
}
