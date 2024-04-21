using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Organization.Commands.DisapproveJoinRequest
{
    public record DisapproveJoinRequestCommand : IRequest<Response>
    {
        public int Id { get; set; }

        public DisapproveJoinRequestCommand(int id)
        {
            Id = id;
        }
    }
}
