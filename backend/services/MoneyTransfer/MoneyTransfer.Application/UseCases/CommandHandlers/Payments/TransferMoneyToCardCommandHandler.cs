using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Results;
using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;
using MediatR;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.CommandHandlers.Payments;

internal sealed class TransferMoneyToCardCommandHandler : IRequestHandler<TransferMoneyToCardCommand, Result<bool>>
{
    private readonly IPaymentRepository _paymentRepository;
    
    public async Task<Result<bool>> Handle(TransferMoneyToCardCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
