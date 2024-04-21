using FindMe.Application.Common.Mapping.Helpers;
using FindMe.Application.Features.UserDetail.Commands.AddDetails;
using FindMe.Application.Features.UserDetail.Commands.UpdateDetails;
using FindMe.Application.Features.UserDetail.Common;
using FindMe.Application.Features.UserRelative.Commands.AddRelatives;
using FindMe.Application.Features.UserRelative.Common;
using FindMe.Domain.Models;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    public class User : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(AddDetailsCommand Command, byte Photo), UserDetails>.NewConfig()
                .Map(dest => dest.MatiralStatus, src => Map.MapMatiralStatus(src.Command.UserDetails.MatiralStatus))
                .Map(dest => dest, src => src.Command.UserDetails)
                .Map(dest => dest.ApplicationUserId, src => src.Commnad.UserId)
                .Map(dest => dest.Photo, src => src.Photo);

            TypeAdapterConfig<UpdateDetailsCommand, UserDetails>.NewConfig()
                .Map(dest => dest.MatiralStatus, src => Map.MapMatiralStatus(src.UserDetails.MatiralStatus))
                .Map(dest=>dest, src=>src.UserDetails)
                .Map(dest => dest.Id, src => src.Id);







            TypeAdapterConfig<(string UserId, UserRelativeDto UserRelativeDto), UserRelatives>.NewConfig()
                .Map(dest => dest.Gendre, src => Map.MapGender(src.UserRelativeDto.Gendre))
                .Map(dest => dest.Relationship, src => Map.MapRelationship(src.UserRelativeDto.Relationship))
                .Map(dest => dest, src => src.UserRelativeDto)
                .Map(dest => dest.ApplicationUserId, src => src.UserId)
                .Ignore(dest => dest.Id);



        }


    }
}
