namespace ch4rniauski.BankApp.MoneyTransfer.Application.Common.Errors;

public sealed class GrpcError : Error
{
    public GrpcError(int statusCode, string message) : base(statusCode, message)
    {
    }
}
