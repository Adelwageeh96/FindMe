using FindMe.Domain.Identity;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using FluentValidation;
using MapsterMapper;
using FindMe.Application.Common.Helpers;
using Microsoft.Extensions.Localization;
using FindMe.Application.Interfaces.Repositories;


namespace FindMe.Application.Features.Profile.Commands
{
    internal class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, Response>
    {
        private readonly IValidator<EditProfileCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<EditProfileCommand> _localization;

        public EditProfileCommandHandler(
            IValidator<EditProfileCommand> validator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<EditProfileCommand> localization)
        {
            _validator = validator;
            _userManager = userManager;
            _mapper = mapper;
            _localization = localization;
        }

        public async Task<Response> Handle(EditProfileCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByIdAsync(command.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this user Id", HttpStatusCode.NotFound);
            }

            _mapper.Map(command.profileDto, user);

            using var dataStream = new MemoryStream();
            await command.profileDto.Photo.CopyToAsync(dataStream);

            byte[] defaultPhotoBytes = ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg");
            if (dataStream.ToArray() == defaultPhotoBytes)
                user.Photo = null;
            else
                user.Photo= dataStream.ToArray();

            await _userManager.UpdateAsync(user);

            return await Response.SuccessAsync(_localization["Success"].Value);

        }
    }
}
