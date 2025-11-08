using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;
using ch4rniauski.BankApp.Authentication.Grpc;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.QueryHandlers.Client;

internal sealed class CheckIfClientExistsQueryHandler : IRequestHandler<CheckIfClientExistsQuery, Result<CheckIfClientExistsResponse>>
{
    private readonly IClientRepository _clientRepository;

    public CheckIfClientExistsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<CheckIfClientExistsResponse>> Handle(CheckIfClientExistsQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var clientId))
        {
            return Result<CheckIfClientExistsResponse>
                .Failure(Error.IncorrectDataType(
                    $"{request.Id} does not match a GUID format"
                    ));
        }
        
        var client = await _clientRepository.GetByIdAsync(clientId, cancellationToken);

        if (client is null)
        {
            return Result<CheckIfClientExistsResponse>
                .Failure(Error.NotFound(
                    $"Client with id {clientId} was not found"
                ));
        }

        var response = new CheckIfClientExistsResponse
        {
            CardHolderName = client.FirstName + " " + client.LastName,
            DoesExist = true
        };

        return Result<CheckIfClientExistsResponse>
                .Success(response);
    }
}
