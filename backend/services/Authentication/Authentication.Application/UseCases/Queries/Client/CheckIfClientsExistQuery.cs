using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;

public sealed record CheckIfClientsExistQuery(string[] Ids) : IRequest<Result<bool[]>>;
