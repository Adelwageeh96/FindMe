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


namespace FindMe.Application.Features.Comments.Commands.Add
{
    internal class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Response>
    {
        private readonly IValidator<AddCommentCommand> _validator;
        private readonly IStringLocalizer<AddCommentCommand> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddCommentCommandHandler(
            IValidator<AddCommentCommand> validator,
            IStringLocalizer<AddCommentCommand> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(AddCommentCommand command, CancellationToken cancellationToken)
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


            var comment = _mapper.Map<Comment>(command);

            await _unitOfWork.Repository<Comment>().AddAsync(comment);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(comment.Id,_stringLocalizer["CommentAddedSuccessfuly"].Value);

        }
    }
}
