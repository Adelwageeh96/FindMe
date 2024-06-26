using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Application.Features.Posts.Commands.Update
{
    public class UpdatePostCommandValidator:AbstractValidator<UpdatePostCommand>
    {
        private readonly IStringLocalizer<UpdatePostCommand> _stringLocalizer;

        public UpdatePostCommandValidator(IStringLocalizer<UpdatePostCommand> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Descripation).NotEmpty();

            RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Photo).NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(11).WithMessage(_stringLocalizer["InvaildPhoneNumber"].Value)
                .Matches("^[0-9]*$").WithMessage(_stringLocalizer["InvaildPhoneNumber"].Value)
                .When(x => x.PhoneNumber != null);
        }
    }
}
