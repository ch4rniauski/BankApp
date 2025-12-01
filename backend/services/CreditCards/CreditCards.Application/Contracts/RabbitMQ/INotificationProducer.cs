using ch4rniauski.BankApp.CreditCards.Domain.Messages;

namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.RabbitMQ;

public interface INotificationProducer : IRabbitMqBaseProducer<NotificationMessage>
{
}
