using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;


namespace FindMe.Application.Features.Comments.Commands.Delete
{
    internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DeleteCommentCommand> _stringLocalizer;

        public DeleteCommentCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DeleteCommentCommand> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Comment>().GetByIdAsync(command.Id) is not Comment comment)
            {
                return await Response.FailureAsync("There is no comment with this Id", HttpStatusCode.NotFound);
            }

            await _unitOfWork.Repository<Comment>().DeleteAsync(comment);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);

        }
    }
}
