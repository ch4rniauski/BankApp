using System.Globalization;
using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Grpc;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Responses.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.MapperProfiles.MoneyTransfers;

internal sealed class TransferMoneyProfile : Profile
{
    public TransferMoneyProfile()
    {
        CreateMap<TransferMoneyRequestDto, TransferMoneyToCardRequest>()
            .ForMember(
                dest => dest.Amount,
                opt => opt.MapFrom(src => src.Amount.ToString(CultureInfo.InvariantCulture)))
            .ForMember(
                dest => dest.ReceiverCardNumber,
                opt => opt.MapFrom(src => src.ReceiverCardNumber))
            .ForMember(
                dest => dest.SenderCardNumber,
                opt => opt.MapFrom(src => src.SenderCardNumber));

        CreateMap<PaymentEntity, TransferMoneyResponseDto>()
            .ConstructUsing(src => new TransferMoneyResponseDto(
                src.Amount,
                src.Currency,
                src.Status,
                src.SenderCardLast4,
                src.ReceiverCardLast4));
    }
}
