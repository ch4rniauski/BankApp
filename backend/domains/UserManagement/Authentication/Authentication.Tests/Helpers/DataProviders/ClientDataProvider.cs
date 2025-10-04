using Bogus;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;

namespace ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;

public static class ClientDataProvider
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
}
