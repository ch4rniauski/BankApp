using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Application.MapperProfiles.Client;

public sealed class RegisterClientProfile : Profile
{
    public RegisterClientProfile()
    {
        CreateMap<RegisterClientRequestDto, ClientEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.IsEmailVerified, opt => opt.MapFrom(_ => false));

        CreateMap<ClientEntity, RegisterClientResponseDto>()
            .ConstructUsing(src => new RegisterClientResponseDto(
                src.FirstName,
                src.LastName,
                src.Email,
                src.PhoneNumber));
    }
}
