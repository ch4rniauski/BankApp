using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Errors;
using ch4rniauski.BankApp.InAppNotifications.Application.Common.Results;
using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;
using ch4rniauski.BankApp.InAppNotifications.Application.UseCases.Queries.Notifications;
using MediatR;

namespace ch4rniauski.BankApp.InAppNotifications.Application.UseCases.QueryHandlers.Notifications;

internal sealed class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Result<GetNotificationByIdResponseDto>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetNotificationByIdQueryHandler(
        IUsersRepository usersRepository,
        IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetNotificationByIdResponseDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var notification = await _usersRepository.GetByIdAsync(request.Id, cancellationToken);

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