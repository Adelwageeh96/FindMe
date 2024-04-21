using FindMe.Application.Features.PinPost.Commands.Pin;
using FindMe.Application.Features.PinPost.Commands.UnPinPost;
using FindMe.Application.Features.PinPost.Queries.GetUserPinnedPosts;
using FindMe.Application.Features.Posts.Commands.Create;
using FindMe.Application.Features.Posts.Commands.Delete;
using FindMe.Application.Features.Posts.Commands.Update;
using FindMe.Application.Features.Posts.Queries.GetAll;
using FindMe.Application.Features.Posts.Queries.GetById;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace FindMe.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync([FromForm]CreatePostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateAsync([FromForm]UpdatePostCommand command, int id)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {
            return Ok(await _mediator.Send(new DeletePostCommand(id)));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<int>>> GetAllAsync([FromQuery]GetAllPostsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new GetPostByIdQuery(id)));
        }

        [HttpPost("Pin")]
        public async Task<ActionResult<Response>> PinAsync(PinPostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UnPin")]
        public async Task<ActionResult<Response>>UnPinAsync(UnPinPostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Pinned")]
        public async Task<ActionResult<Response>> UnPinAsync([FromQuery]GetUserPinnedPostsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


    }
}
