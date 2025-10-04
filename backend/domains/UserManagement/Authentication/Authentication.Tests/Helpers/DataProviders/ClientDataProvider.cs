using Bogus;
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
}