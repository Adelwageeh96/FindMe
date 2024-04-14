using FindMe.Application.Features.Organization.Commands.ApproveJoinRequest;
using FindMe.Application.Features.Organization.Commands.DisapproveJoinRequest;
using FindMe.Application.Features.Organization.Commands.SendJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest;
using FindMe.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FindMe.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController: ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SendJoinReqest")]
        public async Task<ActionResult<string>>SendJoinRequestAsync([FromForm]SendJoinRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = $"{Roles.ADMIN}")]
        [HttpGet("GetAllUnapprovedJoinRequest")]
        public async Task<ActionResult<string>> GetAllUnapprovedJoinRequestAsync(GetAllUnapprovedJoinRequestQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = $"{Roles.ADMIN}")]
        [HttpGet("GetAllApprovedJoinRequest")]
        public async Task<ActionResult<string>> GetAllApprovedJoinRequestAsync(GetAllApprovedJoinRequestQuery query)
        {
            return Ok(await _mediator.Send(query));
        }


        [Authorize(Roles =$"{Roles.ADMIN}")]
        [HttpPost("ApproveJoinRequest/{id}")]
        public async Task<ActionResult<string>> ApproveJoinRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new ApproveJoinRequestCommand(id)));
        }

        [Authorize(Roles = $"{Roles.ADMIN}")]
        [HttpPost("DisapproveJoinRequest/{id}")]
        public async Task<ActionResult<string>> DisapproveJoinRequestAsync(int id)
        {
            return Ok(await _mediator.Send(new DisapproveJoinRequestCommand(id)));
        }


    }
}
