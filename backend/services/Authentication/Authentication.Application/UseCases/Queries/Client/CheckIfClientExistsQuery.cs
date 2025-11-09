using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Grpc;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.Queries.Client;

public sealed record CheckIfClientExistsQuery(string Id) : IRequest<Result<CheckIfClientExistsResponse>>;
