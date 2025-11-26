using Bogus;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;

namespace ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;

internal static class CreditCardDataProvider
{
    public static GetCreditCardResponseDto GenerateGetCreditCardResponseDto()
    {
        return new Faker<GetCreditCardResponseDto>()
            .CustomInstantiator(faker => new GetCreditCardResponseDto(
                Guid.NewGuid(),
                faker.Finance.CreditCardNumber(),
                "Visa",
                faker.Finance.Amount(),
                6,
                3000,
                faker.Person.FullName));
    }
    
    public static IList<GetCreditCardResponseDto> GenerateListOfGetCreditCardResponseDto(int count)
    {
        var faker = new Faker<GetCreditCardResponseDto>()
            .CustomInstantiator(faker => new GetCreditCardResponseDto(
                Guid.NewGuid(),
                faker.Finance.CreditCardNumber(),
                "Visa",
                faker.Finance.Amount(),
                6,
                3000,
                faker.Person.FullName));
        
        return faker.Generate(count);
    }
}
