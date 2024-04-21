using FindMe.Application.Common.Helpers;
using FindMe.Application.Features.PinPost.Commands.Pin;
using FindMe.Application.Features.Posts.Common;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    internal class PinPost : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<PinPostCommand, PinnedPost>.NewConfig()
                .Map(dest => dest.ApplicationUserId, src => src.ActorId);

            TypeAdapterConfig<PinnedPost, PostDto>.NewConfig()
           .Map(dest => dest.Id, src => src.Post.Id)
           .Map(dest => dest.Descripation, src => src.Post.Descripation)
           .Map(dest => dest.Address, src => src.Post.Address)
           .Map(dest => dest.CreatedAt, src => src.Post.CreatedAt)
           .Map(dest => dest.Photo, src => src.Post.Photo)
           .Map(dest => dest.PhoneNumber, src => src.Post.PhoneNumber)
           .Map(dest => dest.Actor, src => src.ApplicationUser.Adapt<ActorInfromationDto>());

            TypeAdapterConfig<ApplicationUser, ActorInfromationDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Photo, src => src.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg"));
        }
    }
}
