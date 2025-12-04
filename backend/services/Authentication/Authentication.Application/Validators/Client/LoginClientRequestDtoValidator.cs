using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using FluentValidation;

namespace ch4rniauski.BankApp.Authentication.Application.Validators.Client;

internal sealed class LoginClientRequestDtoValidator : AbstractValidator<LoginClientRequestDto>
{
    public LoginClientRequestDtoValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
