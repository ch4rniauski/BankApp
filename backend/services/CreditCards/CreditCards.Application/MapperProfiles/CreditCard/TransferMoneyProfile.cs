using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using ch4rniauski.BankApp.CreditCards.Domain.Enums;
using ch4rniauski.BankApp.CreditCards.Grpc;
using Google.Protobuf.WellKnownTypes;

namespace ch4rniauski.BankApp.CreditCards.Application.MapperProfiles.CreditCard;

internal sealed class TransferMoneyProfile : Profile
{
    public TransferMoneyProfile()
    {
        CreateMap<TransferMoneyRequestDto, TransferMoneyResponseDto>()
            .ConvertUsing(src => new TransferMoneyResponseDto(
                PaymentStatusEnum.Success,
                DateTime.UtcNow, 
                src.SenderCardNumber.Substring(src.SenderCardNumber.Length - 4),
                src.ReceiverCardNumber.Substring(src.ReceiverCardNumber.Length - 4),
                src.Amount));
        
        CreateMap<TransferMoneyToCardRequest, TransferMoneyRequestDto>()
            .ConstructUsing(src => new TransferMoneyRequestDto(
                src.ReceiverCardNumber,
                src.SenderCardNumber,
                Guid.Parse(src.ReceiverId),
                Guid.Parse(src.SenderId),
                decimal.Parse(src.Amount)));

        CreateMap<TransferMoneyResponseDto, TransferMoneyToCardResponse>()
            .ForMember(
                dest => dest.Amount,
                opt => opt.MapFrom(src => src.Amount))
            .ForMember(
                dest => dest.PaymentStatus,
                opt => opt.MapFrom(_ => PaymentStatusGrpcEnum.PaymentStatusSuccess))
            .ForMember(
                dest => dest.ProcessedAt,
                opt => opt.MapFrom(src => Timestamp.FromDateTime(src.ProcessedAt)))
            .ForMember(
                dest => dest.SenderCardLast4,
                opt => opt.MapFrom(src => src.SenderCardLast4))
            .ForMember(
                dest => dest.ReceiverCardLast4,
                opt => opt.MapFrom(src => src.ReceiverCardLast4))
            .ForMember(
                dest => dest.ErrorMessage,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.ErrorStatusCode,
                opt => opt.Ignore());

        CreateMap<Error, TransferMoneyToCardResponse>()
            .ForMember(
                dest => dest.ErrorMessage,
                opt => opt.MapFrom(src => src.Message))
            .ForMember(
                dest => dest.ErrorStatusCode,
                opt => opt.MapFrom(src => src.StatusCode))
            .ForMember(
                dest => dest.ProcessedAt,
                opt => opt.MapFrom(_ => Timestamp.FromDateTime(DateTime.UtcNow)))
            .ForMember(
                dest => dest.Amount,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.PaymentStatus,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.SenderCardLast4,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.ReceiverCardLast4,
                opt => opt.Ignore());
    }    
}
