using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;

public sealed record LoginClientCommand(LoginClientRequestDto Request) : IRequest<Result<LoginClientResponseDto>>;
