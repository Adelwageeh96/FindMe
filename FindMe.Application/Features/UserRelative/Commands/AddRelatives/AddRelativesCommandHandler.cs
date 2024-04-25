

using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Constants;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FindMe.Application.Features.UserRelative.Commands.AddRelatives
{
    internal class AddRelativesCommandHandler : IRequestHandler<AddRelativesCommand, Response>
    {
        private readonly IValidator<AddRelativesCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddRelativesCommand> _localization;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddRelativesCommandHandler(
            IValidator<AddRelativesCommand> validator,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IStringLocalizer<AddRelativesCommand> localization,
            UserManager<ApplicationUser> userManager)
        {
            _validator = validator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localization = localization;
            _userManager = userManager;
        }

        public async Task<Response> Handle(AddRelativesCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByIdAsync(command.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this user Id",HttpStatusCode.NotFound);
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.USER))
            {
                return await Response.FailureAsync("Only actors with user role can have relatives");
            }


            var userRelatives = new List<UserRelatives>();
            foreach(var userRelative in command.UserRelatives)
            {
                userRelatives.Add(_mapper.Map<UserRelatives>((command.UserId, userRelative)));
            }
            await _unitOfWork.Repository<UserRelatives>().AddRangeAsync(userRelatives);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(new { UserId = user.Id },_localization["UserRelativesAdded"].Value);
        }
    }
}
