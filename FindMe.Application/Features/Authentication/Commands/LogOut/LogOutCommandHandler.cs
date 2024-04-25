
using FindMe.Domain.Identity;
using FindMe.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;

namespace FindMe.Application.Features.Authentication.Commands.LogOut
{
    public class LogOutCommandHandler : IRequestHandler<LogOutCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<LogOutCommandHandler> _stringLocalizer;
        private readonly IValidator<LogOutCommand> _validator;

        public LogOutCommandHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<LogOutCommandHandler> stringLocalizer, IValidator<LogOutCommand> validator)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
        }

        public async Task<Response> Handle(LogOutCommand command, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (_userManager.Users.FirstOrDefault(u=>u.FCMToken==command.FcmToken)is not ApplicationUser user)
            {
                return await Response.FailureAsync("Invaild Fcm Token",HttpStatusCode.NotFound);
            }

            user.FCMToken = null;
            await _userManager.UpdateAsync(user);

            return await Response.SuccessAsync(_stringLocalizer["LogoutSuccessfully"].Value);
        }
    }
}
