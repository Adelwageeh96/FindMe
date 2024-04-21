using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Application.Features.UserRelative.Commands.UpdateRelatives
{
    internal class UpdateRelativesCommandHandler : IRequestHandler<UpdateRelativesCommand, Response>
    {
        private readonly IValidator<UpdateRelativesCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateRelativesCommand> _stringLocalizer;
        public UpdateRelativesCommandHandler(
            IValidator<UpdateRelativesCommand> validator,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<UpdateRelativesCommand> stringLocalizer)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(UpdateRelativesCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var userRelatives=new List<UserRelatives>();
            foreach(var userRelative in command.UserRelatives)
            {
                if(await _unitOfWork.Repository<UserRelatives>().GetByIdAsync(userRelative.Id) is not UserRelatives relative)
                {
                    return await Response.FailureAsync($"Relative with Id= {userRelative.Id} not found", HttpStatusCode.NotFound);
                }
                _mapper.Map(userRelative, relative);
                userRelatives.Add(relative);
            }

            await _unitOfWork.Repository<UserRelatives>().UpdateRangeAsync(userRelatives);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);
        }
    }
}
