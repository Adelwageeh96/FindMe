using FindMe.Application.Features.Authentication.Commands.AdminRegister;
using FindMe.Application.Features.Organization.Commands.ApproveJoinRequest;
using FindMe.Application.Features.Organization.Commands.DisapproveJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest;
using FindMe.Application.Features.RecognitionRequest.Commands.ApproveRecognitionRequest;
using FindMe.Application.Features.RecognitionRequest.Commands.DisapproveRecognitionRequest;
using FindMe.Application.Features.RecognitionRequest.Queries.GetApprovedRecognitionRequests;
using FindMe.Application.Features.RecognitionRequest.Queries.GetUnpprovedRecognitionRequests;
using FindMe.Application.Features.RecognitionRequest.Queries.GetUserApprovedRecognitionRequests;
using FindMe.Domain.Constants;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FindMe.Presentation.Controller
{
    [Authorize($"{Roles.ADMIN}")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> AdminRegisterAsync(AdminRegisterCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("ApproveJoinRequest/{id}")]
        public async Task<ActionResult<string>> ApproveJoinRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new ApproveJoinRequestCommand(id)));
        }
 
        [HttpPost("DisapproveJoinRequest/{id}")]
        public async Task<ActionResult<string>> DisapproveJoinRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new DisapproveJoinRequestCommand(id)));
        }

        [HttpGet("ApprovedJoinRequests")]
        public async Task<ActionResult<string>> GetAllApprovedJoinRequestAsync([FromQuery]GetAllApprovedJoinRequestQuery query)
        {
            return Ok(await _mediator.Send(query));
        }


        [HttpGet("UnapprovedJoinRequests")]
        public async Task<ActionResult<string>> GetAllUnapprovedJoinRequestAsync([FromQuery]GetAllUnapprovedJoinRequestQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("ApproveIdentificationRequest/{id}")]
        public async Task<ActionResult<string>> ApproveIdentificationRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new ApproveRecognitionRequestCommand(id)));
        }

        [HttpPost("DisapproveIdentificationRequest/{id}")]
        public async Task<ActionResult<string>> DisapproveIdentificationRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new DisapproveRecognitionRequestCommand(id)));
        }

        [HttpGet("ApprovedIdentificationRequests")]
        public async Task<ActionResult<Response>> GetApprovedIdentificationRequestsAsync([FromQuery]ApprovedRecognitionRequestsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [Authorize($"{Roles.ADMIN}")]
        [HttpGet("UnapprovedIdentificationRequests")]
        public async Task<ActionResult<Response>> GetUnapprovedIdentificationRequestsAsync([FromQuery] UnpprovedRecognitionRequestsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }

}
