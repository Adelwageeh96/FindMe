using FindMe.Application.Features.Authentication.Common;
using FindMe.Application.Interfaces.Authentication;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Identity;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
    {
        private readonly IValidator<LoginQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<LoginQueryHandler> _stringLocalizer;
        public LoginQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IStringLocalizer<LoginQueryHandler> stringLocalizer,
            IValidator<LoginQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
        }
        public async Task<Response> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var validationResult= _validator.Validate(query);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var user = await _userManager.FindByEmailAsync(query.UserIdentifier);
            if(user is null)
            {
                user = await _userManager.FindByNameAsync(query.UserIdentifier);
                if (user is null)
                {
                    return await Response.FailureAsync(_stringLocalizer["InvalidLogin"].Value);
                }
            }
            if(!await _userManager.CheckPasswordAsync(user, query.Password))
            {
                return await Response.FailureAsync(_stringLocalizer["InvalidLogin"].Value);
            }
            user.FCMToken = query.FcmToken;
            var role =  _userManager.GetRolesAsync(user).Result.First();
            var token = await _jwtTokenGenerator.GenerateTokenAsync(user, role);

            var response = (role, token, user).Adapt<AuthenticationDto>();

            return await Response.SuccessAsync(response, _stringLocalizer["LoginSuccessfully"]);

        }
    }
}
