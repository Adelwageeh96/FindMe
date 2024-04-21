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


namespace FindMe.Application.Features.Posts.Commands.Create
{
    internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response>
    {
        private readonly IValidator<CreatePostCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<CreatePostCommand> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(
            IValidator<CreatePostCommand> validator,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<CreatePostCommand> stringLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(CreatePostCommand command, CancellationToken cancellationToken)
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
            using var dataStream = new MemoryStream();
            await command.Photo.CopyToAsync(dataStream);

            var post = _mapper.Map<Post>((dataStream.ToArray(),command));

            await _unitOfWork.Repository<Post>().AddAsync(post);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);

        }
    }
}
