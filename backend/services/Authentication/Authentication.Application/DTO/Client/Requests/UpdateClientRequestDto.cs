namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;

public sealed record UpdateClientRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
