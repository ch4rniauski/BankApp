using System.Text;
using System.Text.Json;
using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Services.RabbitMQ.Producers;

public abstract class RabbitMqBaseProducer<T> : IRabbitMqProducer<T> where T : class
{
    protected readonly RabbitMqSettings Settings;
    protected readonly string QueueName;

    protected RabbitMqBaseProducer(
        IOptions<RabbitMqSettings> options,
        string queueName)
    {
        QueueName = queueName;
        Settings = options.Value;
    }

    public async Task PublishAsync(T message,  CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory
        {
            HostName = Settings.Host,
            Port = Settings.Port,
            UserName = Settings.UserName,
            Password = Settings.Password,
        };

        await using var connection = await factory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: QueueName,
            mandatory: true,
            basicProperties: new BasicProperties{ Persistent = true },
            body: body,
            cancellationToken: cancellationToken);
    }
}
