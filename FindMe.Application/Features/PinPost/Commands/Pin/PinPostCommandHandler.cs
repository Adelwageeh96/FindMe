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

namespace FindMe.Application.Features.PinPost.Commands.Pin
{
    internal class PinPostCommandHandler : IRequestHandler<PinPostCommand, Response>
    {
        private readonly IValidator<PinPostCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<PinPostCommand> _stringLocalizer;

        public PinPostCommandHandler(
            IValidator<PinPostCommand> validator,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<PinPostCommand> stringLocalizer)
        {
            _validator = validator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(PinPostCommand command, CancellationToken cancellationToken)
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

            if(!await _unitOfWork.Repository<Post>().AnyAsync(p => p.Id == command.PostId))
            {
                return await Response.FailureAsync("There is no post with this Id", HttpStatusCode.NotFound);
            }

            var pinPost= _mapper.Map<PinnedPost>(command);

            await _unitOfWork.Repository<PinnedPost>().AddAsync(pinPost);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["PostPinnedSuccessfuly"].Value);
        }
    }
}
