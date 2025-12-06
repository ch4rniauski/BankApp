using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Application.MapperProfiles.Client;

internal sealed class UpdateClientProfile : Profile
{
    public UpdateClientProfile()
    {
        CreateMap<UpdateClientRequestDto, ClientEntity>()
            .ForMember(
                dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(
                dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.IsEmailVerified,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.RefreshToken,
                opt => opt.Ignore());

        CreateMap<ClientEntity, UpdateClientResponseDto>()
            .ConstructUsing(src => new UpdateClientResponseDto(
                src.FirstName,
                src.LastName,
                src.Email,
                src.PhoneNumber));
    }
}
