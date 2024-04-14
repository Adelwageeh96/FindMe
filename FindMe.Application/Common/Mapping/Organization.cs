
using FindMe.Application.Features.Organization.Commands.SendJoinRequest;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using Mapster;

namespace FindMe.Application.Common.Mapping
{
    public class Organization : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<SendJoinRequestCommand, OrganizaitonJoinRequest>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Email, src => src.Email)
                .IgnoreNonMapped(true);

            TypeAdapterConfig<OrganizaitonJoinRequest,ApplicationUser>.NewConfig()
                .Map(dest=>dest.Name,src=>src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest=>dest.Address,src=>src.Address)
                .IgnoreNonMapped (true);








        }
    }
}
