using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.QueryHandlers.CreditCards;

internal sealed class GetCreditCardByIdQueryHandler : IRequestHandler<GetCreditCardByIdQuery, Result<GetCreditCardResponseDto>>
{
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IMapper _mapper;

    public GetCreditCardByIdQueryHandler(
        ICreditCardRepository repository,
        IMapper mapper)
    {
        _creditCardRepository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetCreditCardResponseDto>> Handle(GetCreditCardByIdQuery request, CancellationToken cancellationToken)
    {
        var creditCard = await _creditCardRepository.GetByIdAsync(request.CardId, cancellationToken);

        if (creditCard is null)
        {
            return Result<GetCreditCardResponseDto>
                .Failure(Error.NotFound(
                    $"Credit card with ID {request.CardId} was not found"
                    ));
        }
        
        var response = _mapper.Map<GetCreditCardResponseDto>(creditCard);
        
        return Result<GetCreditCardResponseDto>.Success(response);
    }
}
