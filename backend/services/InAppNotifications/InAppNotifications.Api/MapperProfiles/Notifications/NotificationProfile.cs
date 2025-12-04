using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Domain.Messages;
using ch4rniauski.BankApp.InAppNotifications.Domain.Models;

namespace ch4rniauski.BankApp.InAppNotifications.Api.MapperProfiles.Notifications;

internal sealed class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<NotificationMessage, NotificationModel>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(
                dest => dest.Content,
                opt => opt.MapFrom(src => src.Content))
            .ForMember(
                dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(
                dest => dest.IsRead,
                opt => opt.MapFrom(_ => false));
    }
}
