using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;
using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;

namespace ch4rniauski.BankApp.InAppNotifications.Application.MapperProfiles.Notifications;

internal sealed class GetNotificationProfile : Profile
{
    public GetNotificationProfile()
    {
        CreateMap<NotificationEntity, GetNotificationByIdResponseDto>()
            .ConstructUsing(src => new GetNotificationByIdResponseDto(
                src.Id,
                src.Title,
                src.Content,
                src.IsRead));
    }
}
