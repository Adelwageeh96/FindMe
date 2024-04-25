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
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
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
        private readonly IHumanDetectionService _humanDetectionService;
        public AddDetailsCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<AddDetailsCommand> stringLocalizer,
            IValidator<AddDetailsCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHumanDetectionService humanDetectionService)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
            _humanDetectionService = humanDetectionService;
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

            //var response = await _humanDetectionService.VerifyHumanAsync(command.UserDetails.Photo);
            //if (!response.IsSuccess)
            //{
            //    if (response.Message.IsNullOrEmpty())
            //    {
            //        return await Response.FailureAsync(_stringLocalizer["HumanDetectionError"].Value);
            //    }
            //    else
            //        return await Response.FailureAsync($"{response.Message}");
            //}

            //var tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            //await File.WriteAllBytesAsync(tempFilePath,  response.Data);

            //// Log the path of the temporary file to view the photo
            //Console.WriteLine($"Photo saved to: {tempFilePath}");

            //// Open the photo using the default image viewer
            //Process.Start(tempFilePath);

            using var dataStream = new MemoryStream();
            await command.UserDetails.Photo.CopyToAsync(dataStream);

            var userDetails = _mapper.Map<UserDetails>((command,dataStream.ToArray()));
            await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);

            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(new { UserDetailsId= userDetails.Id },_stringLocalizer["UserDetailsAdded"].Value);
        }
    }
}
