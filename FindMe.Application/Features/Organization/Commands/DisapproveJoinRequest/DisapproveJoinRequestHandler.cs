using FindMe.Application.Features.Organization.Commands.ApproveJoinRequest;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Application.Interfaces.Services;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace FindMe.Application.Features.Organization.Commands.DisapproveJoinRequest
{
    public class DisapproveJoinRequestHandler : IRequestHandler<DisapproveJoinRequestCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailingService _mailingService;
        private readonly IStringLocalizer<DisapproveJoinRequestCommand> _stringLocalizer;

        public DisapproveJoinRequestHandler(
            IUnitOfWork unitOfWork,
            IMailingService mailingService,
            IStringLocalizer<DisapproveJoinRequestCommand> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mailingService = mailingService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(DisapproveJoinRequestCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<OrganizaitonJoinRequest>().GetByIdAsync(command.Id) is not OrganizaitonJoinRequest organizaitonJoinRequest)
            {
                return await Response.FailureAsync("There is no request with this Id");
            }

            if (organizaitonJoinRequest.IsApproved)
            {
                return await Response.FailureAsync("Request already approved");
            }
            await _unitOfWork.Repository<OrganizaitonJoinRequest>().DeleteAsync(organizaitonJoinRequest);
            await _unitOfWork.SaveAsync();

            var resourceName = "FindMe.Application.Common.EmailTemplates.OrganizationDisapprovalTemplate.html";
            var assembly = Assembly.GetExecutingAssembly();
            string emailTemplate;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                emailTemplate = await reader.ReadToEndAsync();
            }
            string subject = "FindMe App - Organization Registration Disapproval";
            await _mailingService.SendEmailAsync(organizaitonJoinRequest.Email, subject, emailTemplate);
            return await Response.SuccessAsync(_stringLocalizer["RequestDisapproval"].Value);
        }
    }
}
