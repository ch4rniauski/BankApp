using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Types;
using ch4rniauski.BankApp.InAppNotifications.Application.Extensions;
using ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Queries.Notifications;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Queries;

public sealed class NotificationQuery
{
    public async Task<NotificationType> GetNotificationById(
        string id,
        IMediator mediator,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        var query = new GetNotificationByIdQuery(id);
        
        var result = await mediator.Send(query, cancellationToken);
        
        return result.Match(
            onSuccess: mapper.Map<NotificationType>,
            onFailure: err => throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetCode(err.StatusCode.ToString())
                    .SetMessage(err.Message)
                    .Build()));
    }
}
