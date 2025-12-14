using Bogus;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using ch4rniauski.BankApp.CreditCards.Domain.Enums;

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
        return new Faker<GetCreditCardResponseDto>()
            .CustomInstantiator(faker => new GetCreditCardResponseDto(
                Guid.NewGuid(),
                faker.Finance.CreditCardNumber(),
                "Visa",
                faker.Finance.Amount(),
                6,
                3000,
                faker.Person.FullName))
            .Generate(count);
    }

    public static IList<CreditCardEntity> GenerateListOfCreditCardEntity(int count, Guid cardHolderId)
    {
        return new Faker<CreditCardEntity>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.CardHolderId, _ => cardHolderId)
            .RuleFor(c => c.CardType, _ => CreditCardTypeEnum.Visa.ToString())
            .RuleFor(c => c.IsBlocked, _ => false)
            .RuleFor(c => c.CreatedAt, _ => DateTime.UtcNow.AddDays(-5))
            .RuleFor(c => c.Balance, f => f.Random.Decimal(0, 1000))
            .RuleFor(c => c.CardNumber, f => f.Random.String2(16))
            .RuleFor(c => c.CvvHash, f => f.Finance.CreditCardCvv())
            .RuleFor(c => c.ExpirationMonth, f => f.Random.Byte())
            .RuleFor(c => c.ExpirationYear, f => f.Random.Short((short)DateTime.UtcNow.Year))
            .RuleFor(c => c.PinCodeHash, f => f.Random.String2(10))
            .RuleFor(c => c.CardHolderName, f => f.Person.FullName)
            .Generate(count);
    }
}
