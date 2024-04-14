using FindMe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Presistance.EntityConfiguration
{
    internal class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {

            builder.Property(ud => ud.MatiralStatus)
                .HasConversion<string>();

            builder.HasOne(ud => ud.ApplicationUser)
           .WithOne(u => u.UserDetails)
           .HasForeignKey<UserDetails>(ud => ud.ApplicationUserId)
           .OnDelete(DeleteBehavior.Cascade); 

            builder.ToTable("UserDetails");
        }
    }
}
