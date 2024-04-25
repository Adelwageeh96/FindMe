using FindMe.Application.Features.UserDetail.Commands.AddDetails;
using FindMe.Application.Features.UserDetail.Commands.UpdateDetails;
using FindMe.Application.Features.UserDetail.Common;
using FindMe.Application.Features.UserDetail.Queries.GetDetailsByUserId;
using FindMe.Application.Features.UserRelative.Commands.AddRelatives;
using FindMe.Application.Features.UserRelative.Commands.UpdateRelatives;
using FindMe.Application.Features.UserRelative.Common;
using FindMe.Application.Features.UserRelative.Queries.GetRelatives;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Details")]
        public async Task<ActionResult<string>> AddDetailsAsync([FromForm]AddDetailsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("Details/{userId}")]
        public async Task<ActionResult<string>> UpdateDetailsAsync(string userId,[FromForm]UpdateDetailsCommand command)
        {
            if(userId != command.UserId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<GetUserDetailsDto>> GetDetailsAsync(string id)
        {
            return Ok(await _mediator.Send(new GetDetailsByUserIdQuery(id)));
        }

        [HttpPost("Relatives")]
        public async Task<ActionResult<string>>AddRelativesAsync(AddRelativesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("Relatives")]
        public async Task<ActionResult<string>> UpdateRelativesAsync(UpdateRelativesCommand command)
        {
            
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Relatives/{userId}")]
        public async Task<ActionResult<List<UserRelativeDto>>>GetRelatives(string userId)
        {
            return Ok(await _mediator.Send(new GetRelativesByUserIdQuery(userId)));
        }

    }
}
