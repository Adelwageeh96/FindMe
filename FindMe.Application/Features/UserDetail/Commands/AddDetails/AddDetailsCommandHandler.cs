using FindMe.Application.Common.Helpers;
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


namespace FindMe.Application.Features.UserDetail.Commands.AddDetails
{
    internal class AddDetailsCommandHandler : IRequestHandler<AddDetailsCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddDetailsCommand> _stringLocalizer;
        private readonly IValidator<AddDetailsCommand> _validator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFastApiService _fastApiService;

        public AddDetailsCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<AddDetailsCommand> stringLocalizer,
            IValidator<AddDetailsCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IFastApiService fastApiService)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
            _fastApiService = fastApiService;
        }

        public async Task<Response> Handle(AddDetailsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByIdAsync(command.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this user Id");
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.USER))
            {
                return await Response.FailureAsync("Only actors with user role can have details");
            }

            if (await _unitOfWork.Repository<UserDetails>().AnyAsync(u => u.ApplicationUserId == command.UserId))
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

            using var dataStream = new MemoryStream();
            await command.UserDetails.Photo.CopyToAsync(dataStream);
            var photoBytes = dataStream.ToArray();

            var validateResult = await _fastApiService.ValidatePhotoAsync(photoBytes);
            if (validateResult != "Valid")
            {
                return await Response.FailureAsync("الرجاء إدخال صوره تحتوى على شخص واحد فقط وقريبه من الوجه");
            }

            var embedding = await _fastApiService.GenerateEmbeddingAsync(photoBytes);

            var userDetails = _mapper.Map<UserDetails>(command);
            userDetails.Photo = photoBytes;
            userDetails.EmbeddingVector = EmbeddingVectorConverter.ToByteArray(embedding); 
            user.Address = command.UserDetails.Address;

            await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(new { UserDetailsId = userDetails.Id }, _stringLocalizer["UserDetailsAdded"].Value);
        }
    }

}
