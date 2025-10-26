using ch4rniauski.BankApp.CreditCards.Grpc;
using ch4rniauski.BankApp.MoneyTransfer.Domain.Enums;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.Extensions;

public static class EnumExtensions
{
    public static PaymentStatusEnum ConvertToPaymentStatusEnum(this PaymentStatusGrpcEnum grpcEnum)
        => grpcEnum switch
        {
            PaymentStatusGrpcEnum.PaymentStatusPending => PaymentStatusEnum.Pending,
            PaymentStatusGrpcEnum.PaymentStatusSuccess => PaymentStatusEnum.Success,
            PaymentStatusGrpcEnum.PaymentStatusFailed => PaymentStatusEnum.Failed,
            PaymentStatusGrpcEnum.PaymentStatusCanceled => PaymentStatusEnum.Canceled,
            _ => PaymentStatusEnum.Failed,
        }; 
}
