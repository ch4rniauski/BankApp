using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;

public sealed record CheckIfClientExistsQuery(string Id) : IRequest<Result<bool>>;
