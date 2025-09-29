namespace ch4rniauski.BankApp.Authentication.Domain.Entities;

public class ClientEntity
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public bool IsEmailVerified { get; set; }
    
    public string PhoneNumber { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;
}
