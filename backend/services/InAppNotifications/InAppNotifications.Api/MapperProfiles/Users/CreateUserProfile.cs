using AutoMapper;
using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;
using ch4rniauski.BankApp.InAppNotifications.Domain.Messages;

namespace ch4rniauski.BankApp.InAppNotifications.Api.MapperProfiles.Users;

internal sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<NotificationMessage, UserEntity>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.UserId.ToString()))
            .ForMember(
                dest => dest.Notifications,
                opt => opt.Ignore());
    }
}
