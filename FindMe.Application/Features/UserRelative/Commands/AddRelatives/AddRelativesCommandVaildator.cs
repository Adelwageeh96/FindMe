using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.UserRelative.Commands.AddRelatives
{
    public class AddRelativesCommandVaildator : AbstractValidator<AddRelativesCommand>
    {
        private readonly IStringLocalizer<AddRelativesCommand> _localization;
        public AddRelativesCommandVaildator(IStringLocalizer<AddRelativesCommand> localization)
        {
            _localization = localization;
            RuleFor(r => r.UserId).NotEmpty();

            RuleForEach(x => x.UserRelatives)
              .ChildRules(rel =>
              {
                  rel.RuleFor(r => r.Name).NotEmpty();


                  rel.RuleFor(r => r.PhoneNumber1)
                      .NotEmpty()
                      .Length(11).WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value);

                  rel.RuleFor(r => r.PhoneNumber2)
                      .Cascade(CascadeMode.Stop)
                      .Length(11).WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .When(x => x.PhoneNumber2 == null);

                  rel.RuleFor(r => r.PhoneNumber2)
                      .NotEqual(r => r.PhoneNumber1)
                      .WithMessage(_localization["PhoneNumberEqualityError"].Value);

                  rel.RuleFor(x => x.Gendre)
                   .NotEmpty()
                   .Must(g => Enum.TryParse(typeof(Gendre), g, true, out _)).
                   WithMessage(_localization["GanderException"].Value);

                  rel.RuleFor(r => r.Relationship)
                      .Must(x => Enum.IsDefined(typeof(Relationship), x))
                      .WithMessage(_localization["InvaildRealationship"].Value);
              });
        }
    }
}
