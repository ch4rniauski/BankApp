using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Errors;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Results;
using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Commands.Notifications;
using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;
using ch4rniauski.BankApp.InAppNotifications.Domain.Models;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Application.UseCases.CommandHandlers.Notifications;

internal sealed class ProcessNotificationCommandHandler : IRequestHandler<ProcessNotificationCommand, Result<bool>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public ProcessNotificationCommandHandler(
        IUsersRepository usersRepository,
        IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(ProcessNotificationCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.Message.UserId.ToString(), cancellationToken);

        if (user is null)
        {
            user = _mapper.Map<UserEntity>(request.Message);

            var isCreated = await _usersRepository.CreateAsync(user, cancellationToken);

            if (!isCreated)
            {
                return Result<bool>.Failure(
                    Error.InternalError(
                        $"User with ID {request.Message.UserId} was not created"
                        ));
            }
        }
        
        var notificationModel = _mapper.Map<NotificationModel>(request.Message);
        
        user.Notifications.Add(notificationModel);
        
        var isUpdated = await _usersRepository.UpdateAsync(user.Id, user, cancellationToken: cancellationToken);

        if (!isUpdated)
        {
            return Result<bool>.Failure(
                Error.InternalError(
                    $"User with ID {request.Message.UserId} was not updated"
                ));
        }
        
        return Result<bool>.Success(true);
    }
}
