using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Authentication.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        private readonly IStringLocalizer<UserRegisterCommandValidator> _localization;

        public UserRegisterCommandValidator(IStringLocalizer<UserRegisterCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(_localization["EmailIsNotValidExpression"].Value);

            RuleFor(x => x.PhoneNumber)
                   .NotEmpty()
                   .Length(11).WithMessage(_localization["InvaildPhoneNumber"].Value)
                   .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value);

            RuleFor(x => x.Gendre).NotEmpty().Must(g=>Enum.TryParse(typeof(Gendre), g, true, out _)).WithMessage(_localization["GanderException"].Value);

            RuleFor(x => x.Password).NotEmpty();

        }
    }
}
