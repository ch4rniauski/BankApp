namespace ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.RabbitMQ;

public interface IRabbitMqProducer<in T> where T : class
{
    Task PublishAsync(T message, CancellationToken cancellationToken = default);
}
