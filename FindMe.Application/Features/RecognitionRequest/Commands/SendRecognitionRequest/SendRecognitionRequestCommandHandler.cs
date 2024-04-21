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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest
{
    internal class SendRecognitionRequestCommandHandler : IRequestHandler<SendRecognitionRequestCommand,Response>
    {
        private readonly IValidator<SendRecognitionRequestCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<SendRecognitionRequestCommand> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SendRecognitionRequestCommandHandler(
            IValidator<SendRecognitionRequestCommand> validator,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SendRecognitionRequestCommand> stringLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(SendRecognitionRequestCommand command, CancellationToken cancellationToken)
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

            if(await _userManager.IsInRoleAsync(user, Roles.ADMIN))
            {
                return await Response.FailureAsync(_stringLocalizer["InvaildOperationForThisActor"].Value);
            }
            using var dataStream = new MemoryStream();
            await command.Photo.CopyToAsync(dataStream);

            var recognitionRequest = _mapper.Map<RecognitionRequests>((dataStream.ToArray(), command));

            if(await _userManager.IsInRoleAsync(user, Roles.USER))
            {
                await _unitOfWork.Repository<RecognitionRequests>().AddAsync(recognitionRequest);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                recognitionRequest.IsApproved= true;
                await _unitOfWork.Repository<RecognitionRequests>().AddAsync(recognitionRequest);
                await _unitOfWork.SaveAsync();
            }

            return await Response.SuccessAsync(_stringLocalizer["Success"].Value);

        }
    }
}
