using FindMe.Application.Extention;
using FindMe.Application.Features.Posts.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace FindMe.Application.Features.PinPost.Queries.GetUserPinnedPosts
{
    internal class GetUserPinnedPostsCommandHandler : IRequestHandler<GetUserPinnedPostsCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetUserPinnedPostsCommand> _validator;

        public GetUserPinnedPostsCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<GetUserPinnedPostsCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Response> Handle(GetUserPinnedPostsCommand query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }
            var entities = _unitOfWork.Repository<PinnedPost>().Entities().Where(p => p.ApplicationUserId == query.UserId);

            entities = entities.OrderByDescending(x => x.PinnedAt);
            return await entities.ProjectToType<PostDto>()
                                 .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
