using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using System.Net;
using Microsoft.Extensions.Localization;
using FluentValidation;


namespace FindMe.Application.Features.Comments.Commands.Update
{
    internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<UpdateCommentCommand> _stringLocalizer;
        private readonly IValidator<UpdateCommentCommand> _validator;

        public UpdateCommentCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<UpdateCommentCommand> stringLocalizer,
            IValidator<UpdateCommentCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _validator = validator;
        }

        public async Task<Response> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var validationResult= await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }
            if (await _unitOfWork.Repository<Comment>().GetByIdAsync(command.Id) is not Comment comment)
            {
                return await Response.FailureAsync("There is no comment with this Id", HttpStatusCode.NotFound);
            }

            comment.Content=command.Content;

            await _unitOfWork.Repository<Comment>().UpdateAsync(comment);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(comment.Id,_stringLocalizer["CommentUpdatedSuccessfuly"].Value);


        }
    }
}
