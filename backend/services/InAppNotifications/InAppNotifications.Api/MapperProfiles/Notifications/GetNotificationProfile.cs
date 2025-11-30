using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Types;
using ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;
using ch4rniauski.BankApp.InAppNotifications.Domain.Models;

namespace ch4rniauski.BankApp.InAppNotifications.Api.MapperProfiles.Notifications;

internal sealed class GetNotificationProfile : Profile
{
    public GetNotificationProfile()
    {
        CreateMap<NotificationModel, GetNotificationByIdResponseDto>()
            .ConstructUsing(src => new GetNotificationByIdResponseDto(
                src.Id,
                src.Title,
                src.Content,
                src.IsRead));

        CreateMap<GetNotificationByIdResponseDto, NotificationType>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(
                dest => dest.Content,
                opt => opt.MapFrom(src => src.Content))
            .ForMember(
                dest => dest.IsRead,
                opt => opt.MapFrom(src => src.IsRead));
    }
}
