using FluentValidation;

namespace FindMe.Application.Features.Authentication.Commands.LogOut
{
    public class LogOutCommandValidator : AbstractValidator<LogOutCommand>
    {
        public LogOutCommandValidator()
        {
            RuleFor(x=>x.FcmToken).NotEmpty();
        }
    }
}
