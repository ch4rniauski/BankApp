using System.Globalization;
using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Errors;
using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Results;
using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Grpc;
using Grpc.Net.Client;
using MediatR;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.CommandHandlers.Payments;

internal sealed class TransferMoneyToCardCommandHandler : IRequestHandler<TransferMoneyToCardCommand, Result<bool>>
{
    private readonly IPaymentRepository _paymentRepository;
    
    private const string AuthenticationServiceAddress = "";
    private const string CreditCardServiceAddress = "";

    public TransferMoneyToCardCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<bool>> Handle(TransferMoneyToCardCommand request, CancellationToken cancellationToken)
    {
        // validation
        
        using (var channel = GrpcChannel.ForAddress(AuthenticationServiceAddress))
        {
            var client = new ClientsService.ClientsServiceClient(channel);
            var grpcRequest = new CheckIfClientsExistRequest
            {
                ClientIds =
                {
                    request.Request.SenderId.ToString(),
                    request.Request.ReceiverId.ToString()
                }
            };
            
            var grpcResponse = await client.CheckIfClientsExistAsync(
                request: grpcRequest,
                cancellationToken: cancellationToken);

            if (grpcResponse.DoExist.Any(b => b == false))
            {
                return Result<bool>.Failure(Error.NotFound(
                    "At least one client was not found by the provided IDs"
                    ));
            }
        }
        
        // 2nd
        using (var channel = GrpcChannel.ForAddress(CreditCardServiceAddress))
        {
            var client = new CreditCardsGrpc.CreditCardsGrpcClient(channel);

            var grpcRequest = new TransferMoneyToCardRequest
            {
                Amount = request.Request.Amount.ToString(CultureInfo.InvariantCulture),
                ReceiverCardNumber = request.Request.ReceiverCardNumber,
                SenderCardNumber = request.Request.SenderCardNumber,
                ReceiverId = request.Request.ReceiverId.ToString(),
                SenderId = request.Request.SenderId.ToString(),
            };

            var creditCardGrpcResponse = await client.TransferMoneyToCardAsync(
                request: grpcRequest,
                cancellationToken: cancellationToken);

            if (!creditCardGrpcResponse.IsSuccessful)
            {
                return Result<bool>.Failure(Error.InternalError(
                    "Error occured while transfering money to card"
                    ));
            }
        }
        
        return Result<bool>.Success(true);
    }
}
