using FindMe.Domain.Identity;
using FindMe.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FindMe.Application.Features.Authentication.Commands.ChangePasswrod
{
    internal class ChangePasswordCommandHanlder : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly IValidator<ChangePasswordCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<ChangePasswordCommand> _stringLocalizer;

        public ChangePasswordCommandHanlder(
            IValidator<ChangePasswordCommand> validator,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<ChangePasswordCommand> stringLocalizer)
        {
            _validator = validator;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByIdAsync(command.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this Id", HttpStatusCode.NotFound);
            }

            bool isCorrect = await _userManager.CheckPasswordAsync(user, command.OldPassword);
            if (!isCorrect)
            {
                return await Response.FailureAsync(_stringLocalizer["WrongPassword"].Value);
            }
            var result = await _userManager.ChangePasswordAsync(user,command.OldPassword,command.NewPassword);
            if (!result.Succeeded)
            {
                return await Response.FailureAsync(_stringLocalizer["WrongChangingPassword"].Value,HttpStatusCode.InternalServerError);
            }

            return await Response.SuccessAsync(_stringLocalizer["PasswordChagnedSuccessfuly"].Value);



        }
    }
}
