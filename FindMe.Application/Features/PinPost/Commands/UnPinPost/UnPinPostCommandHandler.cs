using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Constants;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;

namespace FindMe.Application.Features.PinPost.Commands.UnPinPost
{
    internal class UnPinPostCommandHandler : IRequestHandler<UnPinPostCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<UnPinPostCommand> _stringLocalizer;
        private readonly IValidator<UnPinPostCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnPinPostCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<UnPinPostCommand> stringLocalizer,
            IValidator<UnPinPostCommand> validator,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<Response> Handle(UnPinPostCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _userManager.FindByIdAsync(command.ActorId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this Id", HttpStatusCode.NotFound);
            }

            if (await _userManager.IsInRoleAsync(user, Roles.ADMIN))
            {
                return await Response.FailureAsync(_stringLocalizer["InvaildOperationForThisActor"].Value);
            }

            if (!await _unitOfWork.Repository<Post>().AnyAsync(p => p.Id == command.PostId))
            {
                return await Response.FailureAsync("There is no post with this Id", HttpStatusCode.NotFound);
            }

            if (!await _unitOfWork.Repository<PinnedPost>().AnyAsync(pp=>pp.ApplicationUserId==command.ActorId && pp.PostId==command.PostId))
            {
                return await Response.FailureAsync("There is no post pinned with this Id", HttpStatusCode.NotFound);
            }

            var pinnedPost = await _unitOfWork.Repository<PinnedPost>()
                                              .GetItemOnAsync(pp => pp.ApplicationUserId == command.ActorId && pp.PostId == command.PostId); 

            await _unitOfWork.Repository<PinnedPost>().DeleteAsync(pinnedPost);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["PostUnPinnedSuccessfuly"].Value);

        }
    }
}
