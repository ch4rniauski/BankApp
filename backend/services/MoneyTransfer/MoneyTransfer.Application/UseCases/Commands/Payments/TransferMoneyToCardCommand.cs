using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Results;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Responses.Payments;
using MediatR;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;

public sealed record TransferMoneyToCardCommand(TransferMoneyRequestDto Request) : IRequest<Result<TransferMoneyResponseDto>>;
