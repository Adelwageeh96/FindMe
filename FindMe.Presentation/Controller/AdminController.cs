using FindMe.Application.Features.Authentication.Commands.AdminRegister;
using FindMe.Application.Features.Organization.Commands.ApproveJoinRequest;
using FindMe.Application.Features.Organization.Commands.DisapproveJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest;
using FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest;
using FindMe.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FindMe.Presentation.Controller
{
    [Authorize(Roles = $"{Roles.ADMIN}")]
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
    }

}
