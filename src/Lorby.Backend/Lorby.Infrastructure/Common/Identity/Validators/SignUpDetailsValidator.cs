using FluentValidation;
using Lorby.Application.Common.Identity.Models;
using Lorby.Application.Common.Identity.Settings;
using Microsoft.Extensions.Options;

namespace Lorby.Infrastructure.Common.Identity.Validators;

public class SignUpDetailsValidator : AbstractValidator<SignUpDetails>
{
    public SignUpDetailsValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;
        
        RuleFor(details => details.EmailAddress)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(64)
            .Matches(validationSettingsValue.EmailRegexPattern);

        RuleFor(details => details.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(64);

        RuleFor(details => details.Password)
            .MinimumLength(8)
            .MaximumLength(15)
            .Matches(validationSettingsValue.PasswordRegexPattern);
    }
}