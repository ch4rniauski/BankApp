namespace ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;

public sealed record UpdateAccessTokenRequestDto(
    string RefreshToken,
    string ClientId);
