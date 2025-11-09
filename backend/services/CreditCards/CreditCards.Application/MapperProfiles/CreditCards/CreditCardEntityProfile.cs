using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.MapperProfiles.CreditCards;

internal sealed class CreditCardEntityProfile : Profile
{
    public CreditCardEntityProfile()
    {
        CreateMap<CreditCardEntity, GetCreditCardResponseDto>()
            .ConstructUsing(src => new GetCreditCardResponseDto(
                src.Id,
                src.CardNumber,
                src.CardType,
                src.Balance,
                src.ExpirationMonth,
                src.ExpirationYear,
                src.CardHolderName));
    }
}
