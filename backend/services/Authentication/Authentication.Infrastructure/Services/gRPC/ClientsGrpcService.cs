using ch4rniauski.BankApp.Authentication.Application.Extensions;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;
using ch4rniauski.BankApp.Authentication.Grpc;
using Grpc.Core;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Services.gRPC;

public sealed class ClientsGrpcService : ClientsService.ClientsServiceBase
{
    private readonly IMediator _mediator;

    public ClientsGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CheckIfClientExistsResponse> CheckIfClientExists(CheckIfClientExistsRequest request, ServerCallContext context)
    {
        var query = new CheckIfClientExistsQuery(request.ClientId);
        
        var result = await _mediator.Send(query, context.CancellationToken);

        return result.Match(
            onSuccess: _ => new CheckIfClientExistsResponse
            {
                DoesExist = true
            },
            onFailure: _ => new CheckIfClientExistsResponse
            {
                DoesExist = false
            });
    }
}
