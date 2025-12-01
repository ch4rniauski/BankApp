using ch4rniauski.BankApp.CreditCards.Application.Contracts.RabbitMQ;
using ch4rniauski.BankApp.CreditCards.Domain.Messages;
using Microsoft.Extensions.Options;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Services.RabbitMQ.Producers;

internal class NotificationProducer : RabbitMqBaseBaseProducer<NotificationMessage>, INotificationProducer
{
    public NotificationProducer(IOptions<RabbitMqSettings> options) : base(options.Value, "notifications")
    {
    }
}