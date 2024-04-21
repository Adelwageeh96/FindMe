using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.UserDetail.Commands.AddDetails
{
    internal class AddDetailsCommandValidator: AbstractValidator<AddDetailsCommand>
    {
        private readonly IStringLocalizer<AddDetailsCommand> _localization;
        public AddDetailsCommandValidator(IStringLocalizer<AddDetailsCommand> localization)
        {
            _localization = localization;
            RuleFor(x => x.UserDetails.BirthDate).NotEmpty();

            RuleFor(x => x.UserDetails.Job).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();


            RuleFor(x => x.UserDetails.NationalId)
              .NotEmpty()
              .Length(14)
              .Matches("^[0-9]*$").WithMessage(_localization["InvaildNationalNumber"].Value);

            RuleFor(x => x.UserDetails.MatiralStatus)
                .Must(x => Enum.IsDefined(typeof(MatiralStatus), x))
                .WithMessage("Invaild matiral status");

            RuleFor(x => x.UserDetails.PhoneNumber)
               .Cascade(CascadeMode.Stop)
               .Length(11)
               .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value)
               .When(x => x.UserDetails.PhoneNumber == null);

        }
    }
}
