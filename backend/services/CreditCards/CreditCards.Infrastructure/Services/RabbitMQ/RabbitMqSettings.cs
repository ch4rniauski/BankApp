namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Services.RabbitMQ;

public sealed class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Password { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
