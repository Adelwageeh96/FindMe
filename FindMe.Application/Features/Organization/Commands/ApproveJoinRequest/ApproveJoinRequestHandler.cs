using FindMe.Application.Interfaces.Repositories;
using FindMe.Application.Interfaces.Services;
using FindMe.Domain.Constants;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Text;

namespace FindMe.Application.Features.Organization.Commands.ApproveJoinRequest
{
    internal class ApproveJoinRequestHandler : IRequestHandler<ApproveJoinRequestCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<ApproveJoinRequestCommand> _stringLocalizer;
        private readonly IMailingService _mailingService;

        public ApproveJoinRequestHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IMailingService mailingService,
            IStringLocalizer<ApproveJoinRequestCommand> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _mailingService = mailingService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(ApproveJoinRequestCommand command, CancellationToken cancellationToken)
        {
           if(await _unitOfWork.Repository<OrganizaitonJoinRequest>().GetByIdAsync(command.Id) is not OrganizaitonJoinRequest organizaitonJoinRequest)
           {
                return await Response.FailureAsync("There is no request by this Id");
           }

            if (organizaitonJoinRequest.IsApproved)
            {
                return await Response.FailureAsync("Request already approved");
            }

            organizaitonJoinRequest.IsApproved = true;
            var user = _mapper.Map<ApplicationUser>(organizaitonJoinRequest);
            var userName = await GenerateUniqueUsernameAsync(organizaitonJoinRequest.Email);
            var tempPassword =  GenerateTempPassword(10);
            user.UserName = userName;

            var result = await _userManager.CreateAsync(user, tempPassword);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(user, Roles.ORGANIZATION);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            var resourceName = "FindMe.Application.Common.EmailTemplates.OrganizationApprovalTemplate.html";
            var assembly = Assembly.GetExecutingAssembly();
            string emailTemplate;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                emailTemplate = await reader.ReadToEndAsync();
            }
            emailTemplate = emailTemplate.Replace("{Email}", organizaitonJoinRequest.Email);
            emailTemplate = emailTemplate.Replace("{Password}", tempPassword);
            string subject = "Welcome to FindMe App - Admin Role Assignment";
            await _mailingService.SendEmailAsync(organizaitonJoinRequest.Email, subject, emailTemplate);
            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);

        }

        private async Task<string> GenerateUniqueUsernameAsync(string email)
        {
            string username = email.Split('@')[0].ToLower();
            Random random = new();
            while (await _userManager.Users.AnyAsync(u => u.UserName == username))
            {
                int randomSuffix = random.Next(10);
                username += randomSuffix.ToString();
            }
            return username;
        }

        private const string AllChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789^*.?";

        private string GenerateTempPassword(int length)
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
