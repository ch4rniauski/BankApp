using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.QueryHandlers.Client;

internal sealed class CheckIfClientExistsQueryHandler : IRequestHandler<CheckIfClientExistsQuery, Result<bool>>
{
    private readonly IClientRepository _clientRepository;

    public CheckIfClientExistsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<bool>> Handle(CheckIfClientExistsQuery request, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(request.Id, out var clientId))
        {
            return Result<bool>
                .Failure(Error.IncorrectDataType(
                    $"{request.Id} does not match a GUID format"
                    ));
        }
        
        var client = await _clientRepository.GetByIdAsync(clientId, cancellationToken);

        return client is null
            ? Result<bool>
                .Failure(Error.NotFound(
                    $"Client with id {clientId} was not found"
                    ))
            : Result<bool>
                .Success(true);
    }
}
