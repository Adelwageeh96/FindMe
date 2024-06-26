using FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest;
using FindMe.Application.Features.RecognitionRequest.Queries.GetUserApprovedRecognitionRequests;
using FindMe.Application.Features.RecognitionRequest.Queries.GetUserUnapprovedRecognitionRequests;
using FindMe.Application.Features.RecognitionResult.Queries.GetRecognitionRequestResult;
using FindMe.Application.Features.UserDetail.Commands.AddDetails;
using FindMe.Application.Features.UserDetail.Commands.UpdateDetails;
using FindMe.Application.Features.UserDetail.Common;
using FindMe.Application.Features.UserDetail.Queries.GetDetailsByUserId;
using FindMe.Application.Features.UserRelative.Commands.AddRelatives;
using FindMe.Application.Features.UserRelative.Commands.UpdateRelatives;
using FindMe.Application.Features.UserRelative.Common;
using FindMe.Application.Features.UserRelative.Queries.GetRelatives;
using FindMe.Domain.Constants;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace FindMe.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize($"{Roles.USER}")]
        [HttpPost("Details")]
        public async Task<ActionResult<string>> AddDetailsAsync([FromForm] AddDetailsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize($"{Roles.USER}")]
        [HttpPut("Details/{userId}")]
        public async Task<ActionResult<string>> UpdateDetailsAsync(string userId, [FromForm] UpdateDetailsCommand command)
        {
            if (userId != command.UserId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        [Authorize($"{Roles.USER}")]
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<GetUserDetailsDto>> GetDetailsAsync(string id)
        {
            return Ok(await _mediator.Send(new GetDetailsByUserIdQuery(id)));
        }

        [Authorize($"{Roles.USER}")]
        [HttpPost("Relatives")]
        public async Task<ActionResult<string>> AddRelativesAsync(AddRelativesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize($"{Roles.USER}")]
        [HttpPut("Relatives")]
        public async Task<ActionResult<string>> UpdateRelativesAsync(UpdateRelativesCommand command)
        {

            return Ok(await _mediator.Send(command));
        }

        [Authorize($"{Roles.USER}")]
        [HttpGet("Relatives/{userId}")]
        public async Task<ActionResult<List<UserRelativeDto>>> GetRelativesAsync(string userId)
        {
            return Ok(await _mediator.Send(new GetRelativesByUserIdQuery(userId)));
        }

        [Authorize($"UserOrOrganizationPolicy")]
        [HttpPost("IdentificationRequest")]
        public async Task<ActionResult<string>> AddIdentificationRequestAsync([FromForm] SendRecognitionRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize($"UserOrOrganizationPolicy")]
        [HttpGet("ApprovedRecognitionRequests")]
        public async Task<ActionResult<Response>> GetApprovedRecognitionRequestsAsync([FromQuery] GetUserApprovedRecognitionRequestsQuery query)
        {

            return Ok(await _mediator.Send(query));
        }

        [Authorize($"{Roles.USER}")]
        [HttpGet("UnapprovedRecognitionRequests")]
        public async Task<ActionResult<Response>> GetUnapprovedRecognitionRequestsAsync([FromQuery] GetUserUnapprovedRecognitionRequestsQuery query)
        {

            return Ok(await _mediator.Send(query));
        }

        [Authorize($"UserOrOrganizationPolicy")]
        [HttpGet("RecognitionRequestResult/{id}")]
        public async Task<ActionResult<Response>> GetRecognitionRequestResultAsync(int id)
        {
            return Ok(await _mediator.Send(new GetRecognitionRequestResultQuery(id)));
        }

    }
}
