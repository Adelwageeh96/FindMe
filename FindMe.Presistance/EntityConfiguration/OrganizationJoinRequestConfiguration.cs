using FindMe.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace FindMe.Presistance.EntityConfiguration
{
    public class OrganizationJoinRequestConfiguration : IEntityTypeConfiguration<OrganizaitonJoinRequest>
    {
        public void Configure(EntityTypeBuilder<OrganizaitonJoinRequest> builder)
        {


            builder.Property(ojr => ojr.Name)
                .HasMaxLength(200);

            builder.Property(ojr => ojr.Email)
                .HasMaxLength(150);
                

            builder.Property(ojr => ojr.PhoneNumber)
                .HasMaxLength(11);
                

            builder.Property(ojr => ojr.Address)
                .HasMaxLength(400);

            builder.ToTable("OrganizationJoinRequests");
        }
    }
}
