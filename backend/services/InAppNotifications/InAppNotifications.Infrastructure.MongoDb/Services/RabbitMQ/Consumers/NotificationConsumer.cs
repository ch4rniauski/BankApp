using System.Text;
using System.Text.Json;
using ch4rniauski.BankApp.InAppNotifications.Domain.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Services.RabbitMQ.Consumers;

internal sealed class NotificationConsumer : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private IConnection? _connection;
    private IChannel? _channel;
    
    private const string QueueName = "notifications";

    public NotificationConsumer(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            Password = _settings.Password,
            UserName = _settings.UserName,
        };
        
        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            autoDelete: false,
            exclusive: false,
            arguments: null,
            cancellationToken: stoppingToken);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            var notificationMessage = JsonSerializer.Deserialize<NotificationMessage>(message);

            await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(
                deliveryTag: eventArgs.DeliveryTag,
                multiple: false,
                cancellationToken: stoppingToken);
        };
        
        await _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);

        if (_channel is not null)
        {
            await _channel.CloseAsync(cancellationToken);
        }

        if (_connection is not null)
        {
            await _connection.CloseAsync(cancellationToken);
        }
    }
}
