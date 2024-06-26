using FindMe.Application.Common.Mapping.Helpers;
using FindMe.Application.Features.UserDetail.Commands.AddDetails;
using FindMe.Application.Features.UserDetail.Commands.UpdateDetails;
using FindMe.Application.Features.UserDetail.Common;
using FindMe.Application.Features.UserRelative.Common;
using FindMe.Domain.Models;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    public class User : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<AddDetailsCommand, UserDetails>.NewConfig()
                .Map(dest => dest.MatiralStatus, src => Map.MapMatiralStatus(src.UserDetails.MatiralStatus))
                .Map(dest => dest.ApplicationUserId, src => src.UserId)
                .Map(dest => dest, src => src.UserDetails)
                .Ignore(dest => dest.Photo);

            TypeAdapterConfig<(UpdateDetailsCommand Command, byte[] Photo), UserDetails>.NewConfig()
                .Map(dest => dest.MatiralStatus, src => Map.MapMatiralStatus(src.Command.UserDetails.MatiralStatus))
                .Map(dest => dest.ApplicationUser.Address, src => src.Command.UserDetails.Address)
                .Map(dest=>dest.Photo, src => src.Photo)
                .Map(dest => dest, src => src.Command.UserDetails);

            TypeAdapterConfig<UserDetails, GetUserDetailsDto>.NewConfig()
                .Map(dest => dest.UserId, src => src.ApplicationUserId)
                .Map(dest=>dest.Address, src=>src.ApplicationUser.Address)
                .Map(dest => dest, src => src);



            TypeAdapterConfig<UserRelatives, UserRelativeDto>
            .NewConfig()
            .Map(dest => dest.Gendre, src => src.Gendre.ToString())
            .Map(dest => dest.Relationship, src => src.Relationship.ToString());

            TypeAdapterConfig<(string UserId, UserRelativeDto UserRelativeDto), UserRelatives>.NewConfig()
                .Map(dest => dest.Gendre, src => Map.MapGender(src.UserRelativeDto.Gendre))
                .Map(dest => dest.Relationship, src => Map.MapRelationship(src.UserRelativeDto.Relationship))
                .Map(dest => dest, src => src.UserRelativeDto)
                .Map(dest => dest.ApplicationUserId, src => src.UserId)
                .Ignore(dest => dest.Id);
        }
    }
}
