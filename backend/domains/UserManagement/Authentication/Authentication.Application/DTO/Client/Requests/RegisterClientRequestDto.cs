namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;

public sealed record RegisterClientRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password);