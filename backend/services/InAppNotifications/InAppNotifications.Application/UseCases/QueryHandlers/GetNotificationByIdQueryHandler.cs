using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Errors;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Results;
using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;
using ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Queries;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Application.UseCases.QueryHandlers;

internal class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Result<GetNotificationByIdResponseDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetNotificationByIdQueryHandler(
        INotificationRepository notificationRepository,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetNotificationByIdResponseDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByIdAsync(request.Id, cancellationToken);

        if (notification is null)
        {
            return Result<GetNotificationByIdResponseDto>.Failure(Error.NotFound(
                $"No notification with id {request.Id} was not found"
                ));
        }
        
        var result = _mapper.Map<GetNotificationByIdResponseDto>(notification);
        
        return Result<GetNotificationByIdResponseDto>.Success(result);
    }
}