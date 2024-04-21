using FindMe.Application.Common.Helpers;
using FindMe.Application.Features.Posts.Commands.Create;
using FindMe.Application.Features.Posts.Commands.Update;
using FindMe.Application.Features.Posts.Common;
using FindMe.Domain.Models;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    public class Posts : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(byte[] Photo,CreatePostCommand  Post), Post>.NewConfig()
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest.ApplicationUserId, src => src.Post.ActorId)
                .Map(dest => dest, src => src.Post);

            TypeAdapterConfig < (byte[] Photo, UpdatePostCommand Post),Post>.NewConfig()
                .Map(dest=>dest.Photo, src => src.Photo)
                .Map(dest=>dest, src => src.Post);

            TypeAdapterConfig<Post, PostDto>.NewConfig()
            .Map(dest => dest.Actor, src => new ActorInfromationDto
            {
                Id = src.ApplicationUser.Id,
                Name = src.ApplicationUser.Name,
                Photo = src.ApplicationUser.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg")
            });


            TypeAdapterConfig<Post, GetPostByIdDto>.NewConfig()
            .Map(dest => dest.Actor, src => new ActorInfromationDto
            {
                Id = src.ApplicationUser.Id,
                Name = src.ApplicationUser.Name,
                Photo = src.ApplicationUser.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg")
            })
            .Map(dest => dest.Comments, src => src.Comments.Adapt<List<CommentDto>>());

            TypeAdapterConfig<Comment, CommentDto>.NewConfig()
                .Map(dest => dest.Actor, src => new ActorInfromationDto
                {
                    Id = src.ApplicationUser.Id,
                    Name = src.ApplicationUser.Name,
                    Photo = src.ApplicationUser.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg")
                });

        }
    }
}
