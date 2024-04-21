using FindMe.Application.Features.Posts.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MapsterMapper;
using MediatR;
using System.Net;

namespace FindMe.Application.Features.Posts.Queries.GetById
{
    internal class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Post>().GetByIdAsync(query.Id) is not Post post)
            {
                return await Response.FailureAsync("There is no post with this Id", HttpStatusCode.NotFound);
            }

            var postDto = _mapper.Map<GetPostByIdDto>(post);

            return await Response.SuccessAsync(postDto);

        }
    }
}
