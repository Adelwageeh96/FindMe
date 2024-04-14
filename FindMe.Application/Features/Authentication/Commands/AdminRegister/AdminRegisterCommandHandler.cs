using FindMe.Application.Interfaces.Repositories;
using FindMe.Application.Interfaces.Services;
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
using System.Data;
using System.Text;

namespace FindMe.Application.Features.Authentication.Commands.AdminRegister
{
    internal class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, Response>
    {
        private readonly IValidator<AdminRegisterCommand> _validator;
        private readonly IStringLocalizer<AdminRegisterCommand> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailingService _mailingService;

        public AdminRegisterCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<AdminRegisterCommand> stringLocalizer,
            IValidator<AdminRegisterCommand> validator,
            IMailingService mailingService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
            _mailingService = mailingService;
        }

        public async Task<Response> Handle(AdminRegisterCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByEmailAsync(command.Email) is not null)
            {
                return await Response.FailureAsync(_stringLocalizer["EmailExist"].Value);
            }

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == command.PhoneNumber)
               || await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.PhoneNumber == command.PhoneNumber))
            {
                return await Response.FailureAsync(_stringLocalizer["PhoneNumberExist"].Value);
            }
            string userName = await GenerateUniqueUsernameAsync(command.Email);
            
            var user = _mapper.Map<ApplicationUser>((userName, command));
            var tempPassword = GenerateTempPassword(10);
            var result = await _userManager.CreateAsync(user,tempPassword);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(user, Roles.ADMIN);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            string templatePath = "D:/Project/FindMe/FindMe.Application/Common/EmailTemplates/InfromAdminTemplate.html";
            string emailTemplate = File.ReadAllText(templatePath);
            emailTemplate = emailTemplate.Replace("{Email}", command.Email);
            emailTemplate = emailTemplate.Replace("{Password}", tempPassword);
            string subject = "Welcome to FindMe App - Organization Registration Approval";
            await _mailingService.SendEmailAsync(command.Email, subject, emailTemplate);

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

        private const string AllChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789^*.?";

        public string GenerateTempPassword(int length)
        {
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(AllChars[random.Next(AllChars.Length)]);
            }

            return password.ToString();
        }
    }
}
