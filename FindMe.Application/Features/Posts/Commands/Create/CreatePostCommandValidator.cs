using FluentValidation;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.Posts.Commands.Create
{
    internal class CreatePostCommandValidator: AbstractValidator<CreatePostCommand>
    {
        private readonly IStringLocalizer<CreatePostCommand> _localization;
        public CreatePostCommandValidator(IStringLocalizer<CreatePostCommand> localization)
        {
            _localization = localization;

            RuleFor(x => x.Descripation).NotEmpty();

            RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.ActorId).NotEmpty();

            RuleFor(x => x.Photo).NotEmpty();


            RuleFor(x => x.PhoneNumber)
               .Cascade(CascadeMode.Stop)
               .Length(11)
               .Matches("^[0-9]*$").WithMessage(_localization["InvaildPhoneNumber"].Value)
               .When(x => x.PhoneNumber == null);


        }
    }
}
