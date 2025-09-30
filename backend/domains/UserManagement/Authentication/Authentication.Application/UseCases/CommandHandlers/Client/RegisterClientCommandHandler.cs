using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, RegisterClientResponseDto>
{
    public async Task<RegisterClientResponseDto> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}