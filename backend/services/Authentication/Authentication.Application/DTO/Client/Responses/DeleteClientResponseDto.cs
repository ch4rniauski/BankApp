namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;

public sealed record DeleteClientResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
