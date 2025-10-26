using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using FluentValidation;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.Validators.Payments;

public class TransferMoneyRequestDtoValidator : AbstractValidator<TransferMoneyRequestDto>
{
    public TransferMoneyRequestDtoValidator()
    {
        RuleFor(t => t.Amount)
            .GreaterThan(0);
        
        RuleFor(t => t.Currency)
            .NotEmpty()
            .Length(3);

        RuleFor(t => t.ReceiverId)
            .NotEqual(Guid.Empty);
        
        RuleFor(t => t.SenderId)
            .NotEqual(Guid.Empty);
        
        RuleFor(t => t.ReceiverCardNumber)
            .NotEmpty()
            .Length(13, 19)
            .NotEqual(t => t.SenderCardNumber);
        
        RuleFor(t => t.SenderCardNumber)
            .NotEmpty()
            .Length(13, 19)
            .NotEqual(t => t.ReceiverCardNumber);
    }
}
