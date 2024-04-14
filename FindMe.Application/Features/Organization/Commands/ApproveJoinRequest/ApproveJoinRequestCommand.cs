using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Organization.Commands.ApproveJoinRequest
{
    public record ApproveJoinRequestCommand : IRequest<Response>
    {
        public int Id { get; set; }

        public ApproveJoinRequestCommand(int id )
        {
            Id = id;
        }
    }
}
