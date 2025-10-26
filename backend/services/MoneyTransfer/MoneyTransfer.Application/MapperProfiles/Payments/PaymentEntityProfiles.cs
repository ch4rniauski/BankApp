using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Grpc;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.Extensions;
using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.MapperProfiles.Payments;

internal sealed class PaymentEntityProfiles : Profile
{
    public PaymentEntityProfiles()
    {
        CreateMap<TransferMoneyToCardResponse, PaymentEntity>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(
                dest => dest.ProcessedAt,
                opt => opt.MapFrom(src => src.ProcessedAt.ToDateTime()))
            .ForMember(
                dest => dest.Amount,
                opt => opt.MapFrom(src => decimal.Parse(src.Amount)))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.PaymentStatus.ConvertToPaymentStatusEnum()))
            .ForMember(
                dest => dest.ReceiverCardLast4,
                opt => opt.MapFrom(src => src.ReceiverCardLast4))
            .ForMember(
                dest => dest.SenderCardLast4,
                opt => opt.MapFrom(src => src.SenderCardLast4))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<TransferMoneyRequestDto, PaymentEntity>()
            .ForMember(
                dest => dest.ReceiverId,
                opt => opt.MapFrom(src => src.ReceiverId))
            .ForMember(
                dest => dest.SenderId,
                opt => opt.MapFrom(src => src.SenderId))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Currency,
                opt => opt.MapFrom(src => src.Currency))
            .ForAllMembers(opt => opt.Ignore());
    }
}
