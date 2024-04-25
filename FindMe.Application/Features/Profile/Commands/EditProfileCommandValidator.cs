using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.Profile.Commands
{
    internal class EditProfileCommandValidator:AbstractValidator<EditProfileCommand>
    {
        private readonly IStringLocalizer<EditProfileCommand> _stringLocalizer;
        public EditProfileCommandValidator(IStringLocalizer<EditProfileCommand> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x=>x.profileDto.Name).NotEmpty();

            RuleFor(x => x.profileDto.PhoneNumber)
               .NotEmpty()
               .Length(11).WithMessage(_stringLocalizer["InvaildPhoneNumber"].Value)
               .Matches("^[0-9]*$").WithMessage(_stringLocalizer["InvaildPhoneNumber"].Value);

            RuleFor(x => x.profileDto.Address)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .When(x => x.profileDto.Address == null);

            RuleFor(x => x.profileDto.Photo)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .When(x => x.profileDto.Photo == null);

            RuleFor(x => x.profileDto.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(_stringLocalizer["EmailIsNotValidExpression"].Value);

        }
    }
}
