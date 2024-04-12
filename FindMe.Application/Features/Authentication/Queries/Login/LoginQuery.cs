using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Authentication.Queries.Login
{
    public record LoginQuery: IRequest<Response>
    {
        public string UserIdentifier { get; set; } 
        public string Password { get; set; }
    }
}
