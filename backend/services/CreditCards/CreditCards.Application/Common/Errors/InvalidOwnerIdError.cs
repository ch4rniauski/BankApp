namespace ch4rniauski.BankApp.CreditCards.Application.Common.Errors;

public sealed class InvalidOwnerIdError : Error
{
    private const int InvalidOwnerIdErrorStatusCode = 500;
    
    public InvalidOwnerIdError(string message) : base(InvalidOwnerIdErrorStatusCode, message)
    {
    }
}
