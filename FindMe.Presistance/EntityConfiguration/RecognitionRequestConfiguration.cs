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
    internal class RecognitionRequestConfiguration : IEntityTypeConfiguration<RecognitionRequest>
    {
        public void Configure(EntityTypeBuilder<RecognitionRequest> builder)
        {
            builder.Property(rr => rr.Descripation)
               .HasMaxLength(1000);

            builder.HasOne(rr => rr.ApplicationUser)
                   .WithMany(u => u.RecognitionRequests)
                   .HasForeignKey(rr => rr.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(rr => rr.RecognitionRequestResult)
                .WithOne(rrr => rrr.RecognitionRequest)
                .HasForeignKey<RecognitionRequestResult>(rrr => rrr.RecognitionRequestId)
                .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
