using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Results;
using MediatR;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;

public sealed record TransferMoneyToCardCommand() : IRequest<Result<bool>>;
