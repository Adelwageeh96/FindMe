﻿using FindMe.Application.Features.Organization.Commands.ApproveJoinRequest;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Application.Interfaces.Services;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;

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
                return await Response.FailureAsync("There is no request by this Id");
            }

            if (organizaitonJoinRequest.IsApproved)
            {
                return await Response.FailureAsync("Request already approved");
            }
            await _unitOfWork.Repository<OrganizaitonJoinRequest>().DeleteAsync(organizaitonJoinRequest);
            await _unitOfWork.SaveAsync();
            string templatePath = "D:/Project/FindMe/FindMe.Application/Common/EmailTemplates/OrganizationDisapprovalTemplate.html";
            string emailTemplate = File.ReadAllText(templatePath);
            string subject = "FindMe App - Organization Registration Disapproval";
            await _mailingService.SendEmailAsync(organizaitonJoinRequest.Email, subject, emailTemplate);
            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);
        }
    }
}