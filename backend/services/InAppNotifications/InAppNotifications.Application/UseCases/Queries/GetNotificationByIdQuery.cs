using ch4rniauski.BankApp.InAppNotifications.Application.Common.Results;
using ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Queries;

public sealed record GetNotificationByIdQuery(string Id) : IRequest<Result<GetNotificationByIdResponseDto>>;
