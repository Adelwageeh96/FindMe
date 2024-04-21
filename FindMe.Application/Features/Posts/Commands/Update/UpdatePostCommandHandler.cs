using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Application.Features.Posts.Commands.Update
{
    internal class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Response>
    {
        private readonly IValidator<UpdatePostCommand> _validator;
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdatePostCommand> _stringLocalizer;

        public UpdatePostCommandHandler(
            IValidator<UpdatePostCommand> validator,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<UpdatePostCommand> stringLocalizer)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if(await _unitOfWork.Repository<Post>().GetByIdAsync(command.Id)is not Post post)
            {
                return await Response.FailureAsync("There is no post with this Id", HttpStatusCode.NotFound);
            }
            using var dataStream = new MemoryStream();
            await command.Photo.CopyToAsync(dataStream);

            _mapper.Map((dataStream.ToArray(), command), post);

            await _unitOfWork.Repository<Post>().UpdateAsync(post);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);
        }
    }
}
