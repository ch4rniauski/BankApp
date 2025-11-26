using Bogus;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCards;

namespace ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;

internal static class TransferMoneyDataProvider
{
    public static TransferMoneyRequestDto GenerateTransferMoneyRequestDto()
    {
        return new Faker<TransferMoneyRequestDto>()
            .CustomInstantiator(faker => new TransferMoneyRequestDto(
                faker.Finance.CreditCardNumber(),
                faker.Finance.CreditCardNumber(),
                faker.Finance.Amount()));
    }
}
