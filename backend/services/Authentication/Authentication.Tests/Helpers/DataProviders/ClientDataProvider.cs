using Bogus;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;

internal static class ClientDataProvider
{
    public static DeleteClientResponseDto GenerateDeleteClientResponseDto()
    {
        return new Faker<DeleteClientResponseDto>()
            .CustomInstantiator(faker => new DeleteClientResponseDto(
                Guid.NewGuid(),
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email(),
                faker.Person.Phone));
    }
    
    public static LoginClientRequestDto GenerateLoginClientRequestDto()
    {
        return new Faker<LoginClientRequestDto>()
            .CustomInstantiator(faker => new LoginClientRequestDto(
                faker.Internet.Email(),
                faker.Internet.Password()));
    }
        
    public static RegisterClientRequestDto GenerateRegisterClientRequestDto()
    {
        return new Faker<RegisterClientRequestDto>()
            .CustomInstantiator(faker => new RegisterClientRequestDto(
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email(),
                faker.Person.Phone,
                faker.Internet.Password()));
    }
        
    public static RegisterClientResponseDto GenerateRegisterClientResponseDto()
    {
        return new Faker<RegisterClientResponseDto>()
            .CustomInstantiator(faker => new RegisterClientResponseDto(
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email(),
                faker.Person.Phone));
    }
    
    public static UpdateClientRequestDto GenerateUpdateClientRequestDto()
    {
        return new Faker<UpdateClientRequestDto>()
            .CustomInstantiator(faker => new UpdateClientRequestDto(
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email(),
                faker.Person.Phone));
    }
    
    public static UpdateClientResponseDto GenerateUpdateClientResponseDto()
    {
        return new Faker<UpdateClientResponseDto>()
            .CustomInstantiator(faker => new UpdateClientResponseDto(
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email(),
                faker.Person.Phone));
    }
    
    public static ClientEntity GenerateClientEntity()
    {
        return new Faker<ClientEntity>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.IsEmailVerified, _ => false)
            .RuleFor(c => c.FirstName, faker => faker.Person.FirstName)
            .RuleFor(c => c.LastName, faker => faker.Person.LastName)
            .RuleFor(c => c.Email, faker => faker.Person.Email)
            .RuleFor(c => c.PhoneNumber, faker => faker.Person.Phone)
            .RuleFor(c => c.PasswordHash, _ => "Password Hash")
            .RuleFor(c => c.RefreshToken, _ => null!)
            .Generate();
    }
}
