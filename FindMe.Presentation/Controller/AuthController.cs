using FindMe.Application.Features.Authentication.Commands.AdminRegister;
using FindMe.Application.Features.Authentication.Commands.LogOut;
using FindMe.Application.Features.Authentication.Commands.UserRegister;
using FindMe.Application.Features.Authentication.Common;
using FindMe.Application.Features.Authentication.Queries.Login;
using FindMe.Domain.Constants;
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

        [HttpPost("UserRegister")]
        public async Task<ActionResult<string>>UserRegisterAsync(UserRegisterCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles =$"{Roles.ADMIN}")]
        [HttpPost("AdminRegister")]
        public async Task<ActionResult<string>>AdminRegisterAsync(AdminRegisterCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles=$"{Roles.ADMIN},{Roles.USER},{Roles.ORGANIZATION}")]
        [HttpPost("Logout")]
        public async Task<ActionResult<string>>LogoutAsynce(LogOutCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
