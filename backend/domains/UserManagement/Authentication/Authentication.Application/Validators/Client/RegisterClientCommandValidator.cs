using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using FluentValidation;

namespace ch4rniauski.BankApp.Authentication.Application.Validators.Client;

public class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidator()
    {
        RuleFor(c => c.Request.FirstName)
            .NotEmpty()
            .Length(2, 50);
        
        RuleFor(c => c.Request.LastName)
            .NotEmpty()
            .Length(2, 50);
        
        RuleFor(c => c.Request.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Request.PhoneNumber)
            .NotEmpty();
        
        RuleFor(c => c.Request.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}