using FindMe.Application.Features.Authentication.Commands.AdminRegister;
using FindMe.Application.Features.Authentication.Commands.ChangePasswrod;
using FindMe.Application.Features.Authentication.Commands.LogOut;
using FindMe.Application.Features.Authentication.Commands.UserRegister;
using FindMe.Application.Features.Authentication.Common;
using FindMe.Application.Features.Authentication.Queries.Login;
using FindMe.Application.Features.Organization.Commands.SendJoinRequest;
using FindMe.Application.Features.Profile.Commands;
using FindMe.Application.Features.Profile.Common;
using FindMe.Application.Features.Profile.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FindMe.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDto>> LoginAsync(LoginQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>>RegisterAsync(UserRegisterCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("OrganizationJoinReqest")]
        public async Task<ActionResult<string>>SendJoinRequestAsync([FromForm] SendJoinRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult<string>>LogoutAsynce(LogOutCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [Authorize]
        [HttpGet("Profile/{id}")]
        public async Task<ActionResult<ProfileDto>>GetProfileAsync(string id)
        {
            return Ok(await _mediator.Send(new GetProfileQuery(id)));
        }
        [Authorize]
        [HttpPut("Profile")]
        public async Task<ActionResult<string>>UpdateProfileAsync([FromForm]EditProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<string>>ChangePasswordAsync(ChangePasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }




    }
}
