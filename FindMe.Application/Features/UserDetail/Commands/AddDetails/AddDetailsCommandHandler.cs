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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Application.Features.UserDetail.Commands.AddDetails
{
    internal class AddDetailsCommandHandler : IRequestHandler<AddDetailsCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddDetailsCommand> _stringLocalizer;
        private readonly IValidator<AddDetailsCommand> _validator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddDetailsCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<AddDetailsCommand> stringLocalizer,
            IValidator<AddDetailsCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<Response> Handle(AddDetailsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if(await _userManager.FindByIdAsync(command.UserId)is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this user Id");
            }

            if(!await _userManager.IsInRoleAsync(user, Roles.USER))
            {
                return await Response.FailureAsync("Only actors with user role can have details");
            }

            if(await _unitOfWork.Repository<UserDetails>().AnyAsync(u => u.ApplicationUserId == command.UserId))
            {
                return await Response.FailureAsync("User already have details");
            }

            if (await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.NationalId == command.UserDetails.NationalId))
            {
                return await Response.FailureAsync(_stringLocalizer["NationalNumberExist"].Value);
            }

            if (command.UserDetails.PhoneNumber is not null && (await _userManager.Users.AnyAsync(u => u.PhoneNumber == command.UserDetails.PhoneNumber)
              || await _unitOfWork.Repository<UserDetails>().AnyAsync(ud => ud.PhoneNumber == command.UserDetails.PhoneNumber)))
            {
                return await Response.FailureAsync(_stringLocalizer["PhoneNumberExist"].Value);
            }

            var userDetails = _mapper.Map<UserDetails>(command);
            await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);

            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);
        }
    }
}
