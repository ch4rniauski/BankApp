using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.QueryHandlers.Client;

internal sealed class UpdateAccessTokenQueryHandler : IRequestHandler<UpdateAccessTokenQuery, Result<UpdateAccessTokenResponseDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly ITokenProvider _tokenProvider;

    public UpdateAccessTokenQueryHandler(
        IClientRepository clientRepository,
        ITokenProvider tokenProvider)
    {
        _clientRepository = clientRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<UpdateAccessTokenResponseDto>> Handle(UpdateAccessTokenQuery request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            return Result<UpdateAccessTokenResponseDto>
                .Failure(Error.NotFound(
                    "ID was not found in the provided request"
                    ));
        }

        if (!Guid.TryParse(request.Id, out var id))
        {
            return Result<UpdateAccessTokenResponseDto>
                .Failure(Error.IncorrectDataType(
                    "Provided ID does not match Guid format"
                ));
        }
        
        var client = await _clientRepository.GetByIdAsync(id, cancellationToken);

        if (client is null)
        {
            return Result<UpdateAccessTokenResponseDto>
                .Failure(Error.NotFound(
                    $"Client with ID '{id}' was not found"
                ));
        }

        if (client.RefreshToken != request.RefreshToken)
        {
            return Result<UpdateAccessTokenResponseDto>
                .Failure(Error.IncorrectToken(
                    $"Provided Refresh Token '{request.RefreshToken}' is invalid"
                ));
        }
        
        var updatedAccessToken = _tokenProvider.GenerateAccessToken(client);
        var response = new UpdateAccessTokenResponseDto(updatedAccessToken);
        
        return Result<UpdateAccessTokenResponseDto>.Success(response);
    }
}