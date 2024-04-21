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
                      .Length(11)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value);

                  rel.RuleFor(r => r.PhoneNumber2)
                      .Cascade(CascadeMode.Stop)
                      .NotEmpty()
                      .When(r => !string.IsNullOrEmpty(r.PhoneNumber2))
                      .Length(11)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value);


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
