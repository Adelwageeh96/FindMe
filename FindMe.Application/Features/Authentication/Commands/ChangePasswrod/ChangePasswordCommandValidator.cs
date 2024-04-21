using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.Authentication.Commands.ChangePasswrod
{
    internal class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
    {
        private readonly IStringLocalizer<ChangePasswordCommand> _localization;
        public ChangePasswordCommandValidator(IStringLocalizer<ChangePasswordCommand> localization)
        {
            _localization = localization;

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.OldPassword).NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .NotEqual(x => x.OldPassword)
                .WithMessage(_localization["NewAndOldPasswordcanNotBeSame"].Value);

        }
    }
}
