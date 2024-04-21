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

namespace FindMe.Application.Features.UserDetail.Commands.UpdateDetails
{
    internal class UpdateDetailsCommandHandler : IRequestHandler<UpdateDetailsCommand, Response>
    {
        private readonly IValidator<UpdateDetailsCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<UpdateDetailsCommand> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateDetailsCommandHandler(
            IValidator<UpdateDetailsCommand> validator,
            IUnitOfWork unitOfWork,
            IStringLocalizer<UpdateDetailsCommand> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateDetailsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _unitOfWork.Repository<UserDetails>().GetByIdAsync(command.Id) is not UserDetails userDetails)
            {
                return await Response.FailureAsync("Id don't exist");
            }

            if (userDetails.NationalId !=command.UserDetails.NationalId && await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.NationalId == command.UserDetails.NationalId))
            {
                return await Response.FailureAsync(_stringLocalizer["NationalNumberExist"].Value);
            }

            if (command.UserDetails.PhoneNumber is not null && userDetails.PhoneNumber != command.UserDetails.PhoneNumber &&(await _userManager.Users.AnyAsync(u => u.PhoneNumber == command.UserDetails.PhoneNumber)
              || await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.PhoneNumber == command.UserDetails.PhoneNumber)))
            {
                return await Response.FailureAsync(_stringLocalizer["PhoneNumberExist"].Value);
            }

             _mapper.Map(command.UserDetails, userDetails);

            await _unitOfWork.Repository<UserDetails>().UpdateAsync(userDetails);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(userDetails.Id, _stringLocalizer["Success"].Value);
        }
    }
}
