using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Constants;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Authentication.Commands.UserRegister
{
    internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Response>
    {
        private readonly IValidator<UserRegisterCommand> _validator;
        private readonly IStringLocalizer<UserRegisterCommand> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRegisterCommandHandler(
            IValidator<UserRegisterCommand> validator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<UserRegisterCommand> stringLocalizer,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userManager = userManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if(await _userManager.FindByEmailAsync(command.Email)is not null)
            {
                return await Response.FailureAsync(_stringLocalizer["EmailExist"].Value);
            }

            if(await _userManager.Users.AnyAsync(u=>u.PhoneNumber==command.PhoneNumber)
               || await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.PhoneNumber == command.PhoneNumber))
            {
                return await Response.FailureAsync(_stringLocalizer["PhoneNumberExist"].Value);
            }

            string userName = await GenerateUniqueUsernameAsync(command.Email);

            var user = _mapper.Map<ApplicationUser>((userName, command));

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(user, Roles.USER);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            return await Response.SuccessAsync(_stringLocalizer["AccountCreatedSuccessfully"].Value);
        }


        private async Task<string> GenerateUniqueUsernameAsync(string email)
        {
            string username = email.Split('@')[0];
            Random random = new();
            while (await _userManager.Users.AnyAsync(u => u.UserName == username))
            {
                int randomSuffix = random.Next(10); 
                username += randomSuffix.ToString();
            }
            return username;
        }
    }
}
