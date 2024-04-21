using FindMe.Application.Features.Comments.Commands.Add;
using FindMe.Application.Features.Comments.Commands.Delete;
using FindMe.Application.Features.Comments.Commands.Update;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FindMe.Presentation.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Response>>AddAsync(AddCommentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>>UpdateAsync(int id,  UpdateCommentCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>>DeleteAsync(int id)
        {
            return Ok(await _mediator.Send(new DeleteCommentCommand(id)));
        }


    }
}
