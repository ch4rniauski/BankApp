using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using ch4rniauski.BankApp.CreditCards.Domain.Enums;

namespace ch4rniauski.BankApp.CreditCards.Application.MapperProfiles.CreditCard;

internal sealed class TransferMoneyProfile : Profile
{
    public TransferMoneyProfile()
    {
        CreateMap<TransferMoneyRequestDto, TransferMoneyResponseDto>()
            .ConstructUsing(src => new TransferMoneyResponseDto(
                PaymentStatusEnum.Success,
                DateTime.UtcNow, 
                src.SenderCardNumber[^4..],
                src.ReceiverCardNumber[^4..],
                src.Amount));
    }    
}
