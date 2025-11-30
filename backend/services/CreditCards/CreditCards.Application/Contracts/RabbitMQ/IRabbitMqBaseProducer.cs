namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.RabbitMQ;

public interface IRabbitMqBaseProducer<in T> where T : class
{
    Task PublishAsync(T message, CancellationToken cancellationToken = default);
}
