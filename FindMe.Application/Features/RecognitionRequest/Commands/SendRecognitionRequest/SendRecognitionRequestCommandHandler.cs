using FindMe.Application.Features.RecognitionRequest.Commands.ApproveRecognitionRequest;
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



namespace FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest
{
    internal class SendRecognitionRequestCommandHandler : IRequestHandler<SendRecognitionRequestCommand,Response>
    {
        private readonly IValidator<SendRecognitionRequestCommand> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<SendRecognitionRequestCommand> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IFastApiService _fastApiService;

        public SendRecognitionRequestCommandHandler(
            IValidator<SendRecognitionRequestCommand> validator,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SendRecognitionRequestCommand> stringLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IFastApiService fastApiService)
        {
            _validator = validator;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _fastApiService = fastApiService;
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
            var validateResult = await _fastApiService.ValidatePhotoAsync(dataStream.ToArray());
            if (validateResult != "Valid")
            {
                return await Response.FailureAsync("الرجاء إدخال صوره تحتوى على شخص واحد فقط وقريبه من الوجه");
            }

            var recognitionRequest = _mapper.Map<RecognitionRequests>((command, dataStream.ToArray()));

            await _unitOfWork.Repository<RecognitionRequests>().AddAsync(recognitionRequest);
            await _unitOfWork.SaveAsync();

            if (await _userManager.IsInRoleAsync(user, Roles.ORGANIZATION))
            {
                var response = await _mediator.Send(new ApproveRecognitionRequestCommand(recognitionRequest.Id));
            }

            return await Response.SuccessAsync(_stringLocalizer["IdentificationRequestSent"].Value);

        }
    }
}
