using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Organization.Commands.SendJoinRequest
{
    public class SendJoinRequestCommandValidator: AbstractValidator<SendJoinRequestCommand>
    {
        private readonly IStringLocalizer<SendJoinRequestCommandValidator> _localization;
        public SendJoinRequestCommandValidator(IStringLocalizer<SendJoinRequestCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(_localization["EmailIsNotValidExpression"].Value);
            RuleFor(x => x.PhoneNumber).NotEmpty().Must(ph => ph.Length == 11).WithMessage(_localization["InvaildPhoneNumber"].Value);
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x=>x.RequestPhoto).NotEmpty();

        }
    }
}
