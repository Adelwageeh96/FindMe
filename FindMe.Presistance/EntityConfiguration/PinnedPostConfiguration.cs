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
    internal class PinnedPostConfiguration : IEntityTypeConfiguration<PinnedPost>
    {
        public void Configure(EntityTypeBuilder<PinnedPost> builder)
        {
            builder.HasOne(pp => pp.Post)
                .WithMany()
                .HasForeignKey(pp => pp.PostId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            builder.HasOne(pp => pp.ApplicationUser)
                .WithMany(u => u.PinnedPosts)
                .HasForeignKey(pp => pp.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
