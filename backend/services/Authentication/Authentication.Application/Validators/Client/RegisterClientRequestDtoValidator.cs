using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using FluentValidation;

namespace ch4rniauski.BankApp.Authentication.Application.Validators.Client;

internal sealed class RegisterClientRequestDtoValidator : AbstractValidator<RegisterClientRequestDto>
{
    public RegisterClientRequestDtoValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .Length(2, 50);
        
        RuleFor(c => c.LastName)
            .NotEmpty()
            .Length(2, 50);
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.PhoneNumber)
            .NotEmpty();
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
