using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.Organization.Commands.SendJoinRequest
{
    internal class SendJoinRequestCommandHandler : IRequestHandler<SendJoinRequestCommand, Response>
    {
        private readonly IValidator<SendJoinRequestCommand> _validator;
        private readonly IStringLocalizer<SendJoinRequestCommand> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SendJoinRequestCommandHandler(
            IValidator<SendJoinRequestCommand> validator,
            IStringLocalizer<SendJoinRequestCommand> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _validator = validator;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(SendJoinRequestCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByEmailAsync(command.Email) is not null
                || await _unitOfWork.Repository<OrganizaitonJoinRequest>().AnyAsync(ojr=>ojr.Email==command.Email))
            {
                return await Response.FailureAsync(_stringLocalizer["EmailExist"].Value);
            }

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == command.PhoneNumber)
               || await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.PhoneNumber == command.PhoneNumber)
               || await _unitOfWork.Repository<OrganizaitonJoinRequest>().AnyAsync(ojr => ojr.PhoneNumber == command.PhoneNumber))
            {
                return await Response.FailureAsync(_stringLocalizer["PhoneNumberExist"].Value);
            }
            using var dataStream = new MemoryStream();
            await command.RequestPhoto.CopyToAsync(dataStream);

            var organizaitionJoinRequest = _mapper.Map<OrganizaitonJoinRequest>(command);
            organizaitionJoinRequest.IsApproved = false;
            organizaitionJoinRequest.RequestPhoto=dataStream.ToArray();

            await _unitOfWork.Repository<OrganizaitonJoinRequest>().AddAsync(organizaitionJoinRequest);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["JoinRequestSendSuccessfully"].Value);
        }
    }
}
