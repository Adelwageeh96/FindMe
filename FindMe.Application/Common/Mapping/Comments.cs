using FindMe.Application.Features.Comments.Commands.Add;
using FindMe.Domain.Models;
using Mapster;

namespace FindMe.Application.Common.Mapping
{
    internal class Comments : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<AddCommentCommand, Comment>.NewConfig()
                .Map(dest => dest.ApplicationUserId, src => src.ActorId);
        }
    }
}
