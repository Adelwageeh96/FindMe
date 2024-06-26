using FluentValidation;

namespace FindMe.Application.Features.Authentication.Queries.Login
{
    public class LoginQueryVaildator : AbstractValidator<LoginQuery>
    {
        public LoginQueryVaildator()
        {
              RuleFor(x=>x.UserIdentifier).NotEmpty();
              RuleFor(x=>x.Password).NotEmpty();
              RuleFor(x=>x.FCMToken).NotEmpty();
        }
    }
}
