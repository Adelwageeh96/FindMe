﻿using FindMe.Domain.Constants;
using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.UserRelative.Commands.UpdateRelatives
{
    internal class UpdateRelativesCommandValidator: AbstractValidator<UpdateRelativesCommand>
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