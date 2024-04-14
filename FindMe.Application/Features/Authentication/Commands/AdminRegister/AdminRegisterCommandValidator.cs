using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Authentication.Commands.AdminRegister
{
    public class AdminRegisterCommandValidator : AbstractValidator<AdminRegisterCommand>
    {
        private readonly IStringLocalizer<AdminRegisterCommandValidator> _localization;

        public AdminRegisterCommandValidator(IStringLocalizer<AdminRegisterCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(_localization["EmailIsNotValidExpression"].Value);
            RuleFor(x => x.PhoneNumber).NotEmpty().Must(ph => ph.Length == 11).WithMessage(_localization["InvaildPhoneNumber"].Value);
            RuleFor(x => x.Gendre).NotEmpty().Must(g => Enum.TryParse(typeof(Gendre), g, true, out _)).WithMessage(_localization["GanderException"].Value);
        }
    }
}
