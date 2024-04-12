using FindMe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Presistance.EntityConfiguration
{
    internal class UserRelativesConfiguration : IEntityTypeConfiguration<UserRelatives>
    {
        public void Configure(EntityTypeBuilder<UserRelatives> builder)
        {
            builder.Property(ur => ur.Gendre)
            .HasConversion<string>(); 

            
            builder.Property(ur => ur.Relationship)
                .HasConversion<string>();

            builder.HasOne(ur => ur.ApplicationUser)
           .WithMany(u => u.UserRelatives)
           .HasForeignKey(ur => ur.ApplicationUserId)
           .OnDelete(DeleteBehavior.Restrict); 


            builder.ToTable("UserRelatives");
        }
    }
}
