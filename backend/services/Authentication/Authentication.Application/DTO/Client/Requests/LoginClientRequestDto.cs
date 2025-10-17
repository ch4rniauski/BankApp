namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;

public sealed record LoginClientRequestDto(
    string Email,
    string Password);
    