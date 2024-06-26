using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.UserRelative.Commands.UpdateRelatives
{
    public class UpdateRelativesCommandValidator: AbstractValidator<UpdateRelativesCommand>
    {
        private readonly IStringLocalizer<UpdateRelativesCommand> _localization;
        public UpdateRelativesCommandValidator(IStringLocalizer<UpdateRelativesCommand> localization)
        {
            _localization = localization;
            RuleForEach(x => x.UserRelatives)
              .ChildRules(rel =>
              {
                  rel.RuleFor(r => r.Name).NotEmpty();
                  rel.RuleFor(r => r.Id).NotEmpty();

                  rel.RuleFor(r => r.PhoneNumber1)
                      .NotEmpty()
                      .Length(11).WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value);

                  rel.RuleFor(r => r.PhoneNumber2)
                      .Cascade(CascadeMode.Stop)
                      .Length(11).WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value)
                      .When(x => x.PhoneNumber2 == null);


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
