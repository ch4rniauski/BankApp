using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Application.MapperProfiles.Client;

public sealed class DeleteClientProfile : Profile
{
    public DeleteClientProfile()
    {
        CreateMap<ClientEntity, DeleteClientResponseDto>()
            .ConstructUsing(src => new DeleteClientResponseDto(
                src.Id,
                src.FirstName,
                src.LastName,
                src.Email,
                src.PhoneNumber));
    }
}