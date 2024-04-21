using FindMe.Application.Extention;
using FindMe.Application.Features.Organization.Queries.Common;
using FindMe.Application.Features.Posts.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FindMe.Application.Features.Posts.Queries.GetAll
{
    internal class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, Response>
    {
        private readonly IValidator<GetAllPostsQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPostsQueryHandler(
            IValidator<GetAllPostsQuery> validator,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetAllPostsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }
            var entities = _unitOfWork.Repository<Post>().Entities();

            if (!query.KeyWord.IsNullOrEmpty())
            {
                entities = entities.Where(x => x.Descripation.ToLower().Contains(query.KeyWord.ToLower()));
            }

            entities = entities.OrderByDescending(x => x.CreatedAt);

            return await entities.ProjectToType<PostDto>()
                                 .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
