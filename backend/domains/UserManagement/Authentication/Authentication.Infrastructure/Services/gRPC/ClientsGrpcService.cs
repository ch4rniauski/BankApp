using ch4rniauski.BankApp.Authentication.Grpc;
using Grpc.Core;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Services.gRPC;

public sealed class ClientsGrpcService : ClientsService.ClientsServiceBase
{
    public override async Task<CheckIfClientExistsResponse> CheckIfClientExists(CheckIfClientExistsRequest request, ServerCallContext context)
    {
        
        return new CheckIfClientExistsResponse();
    }
}
