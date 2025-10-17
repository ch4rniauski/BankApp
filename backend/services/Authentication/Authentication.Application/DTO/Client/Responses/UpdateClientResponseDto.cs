namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;

public sealed record UpdateClientResponseDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
