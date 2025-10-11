namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;

public sealed record LoginClientResponseDto(
    string AccessToken,
    string RefreshToken);
