using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;

namespace FindMe.Application.Features.Posts.Commands.Delete
{
    internal class DeletePostCommandHanlder : IRequestHandler<DeletePostCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DeletePostCommand> _stringLocalizer;

        public DeletePostCommandHanlder(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DeletePostCommand> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Post>().GetByIdAsync(command.Id) is not Post post)
            {
                return await Response.FailureAsync("There is no post with this Id", HttpStatusCode.NotFound);
            }

            await _unitOfWork.Repository<Post>().DeleteAsync(post);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);
        }
    }
}
