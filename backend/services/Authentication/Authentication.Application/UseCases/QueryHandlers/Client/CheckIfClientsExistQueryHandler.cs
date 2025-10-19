using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.QueryHandlers.Client;

internal sealed class CheckIfClientsExistQueryHandler : IRequestHandler<CheckIfClientsExistQuery, Result<bool[]>>
{
    private readonly IClientRepository _clientRepository;

    public CheckIfClientsExistQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<bool[]>> Handle(CheckIfClientsExistQuery request, CancellationToken cancellationToken)
    {
        var resultArray = GC.AllocateUninitializedArray<bool>(request.Ids.Length);

        for (var i = 0; i < request.Ids.Length; i++)
        {
            var id = Guid.Parse(request.Ids[i]);
            
            var client = await _clientRepository.GetByIdAsync(id, cancellationToken);

            resultArray[i] = client is not null;
        }
        
        return Result<bool[]>.Success(resultArray);
    }
}
