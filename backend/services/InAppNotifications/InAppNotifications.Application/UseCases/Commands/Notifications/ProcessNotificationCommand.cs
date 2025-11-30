using ch4rniauski.BankApp.InAppNotifications.Application.Common.Results;
using ch4rniauski.BankApp.InAppNotifications.Domain.Messages;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Commands.Notifications;

public sealed record ProcessNotificationCommand(NotificationMessage Message) : IRequest<Result<bool>>;
