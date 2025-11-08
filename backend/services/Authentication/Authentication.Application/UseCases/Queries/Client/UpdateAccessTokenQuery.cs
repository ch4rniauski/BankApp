using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;

public sealed record UpdateAccessTokenQuery(
    string? Id,
    string RefreshToken) : IRequest<Result<UpdateAccessTokenResponseDto>>;
