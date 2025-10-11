using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.MapperProfiles.CreditCard;

public sealed class CreateCreditCardProfile : Profile
{
    public CreateCreditCardProfile()
    {
        CreateMap<CreateCreditCardRequestDto, CreditCardEntity>()
            .ForMember(
                dest => dest.Id,
                _ => Guid.NewGuid()
                )
            .ForMember(dest => dest.CardNumber,
                _ => string.Concat(
                    Enumerable.Range(0, 16)
                        .Select(_ => Random.Shared.Next(0, 16))
                        .ToString()
                    )
            )
            .ForMember(
                dest => dest.CardType,
                opt => opt.MapFrom(src => src.CardType.ToString())
                )
            .ForMember(
                dest => dest.ExpirationYear,
                opt => opt.MapFrom(_ => DateTime.UtcNow.Year + 4)
                )
            .ForMember(
                dest => dest.ExpirationMonth,
                opt => opt.MapFrom(_ => DateTime.UtcNow.Month)
                )
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
                )
            .ForMember(
                dest => dest.IsBlocked,
                opt => opt.MapFrom(_ => false)
                )
            .ForMember(
                dest => dest.CardHolderId,
                opt => opt.MapFrom(src => src.CardHolderId)
                );

        CreateMap<CreditCardEntity, CreateCreditCardResponseDto>()
            .ConstructUsing(src => new CreateCreditCardResponseDto(
                src.Id,
                src.CardNumber,
                src.CardType,
                src.ExpirationMonth,
                src.ExpirationYear,
                src.CardHolderName));
    }
}
