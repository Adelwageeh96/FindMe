using FindMe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindMe.Presistance.EntityConfiguration
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Descripation)
                .HasMaxLength(1000);

            builder.Property(p => p.Address)
                .HasMaxLength(400);

            builder.HasOne(p => p.ApplicationUser)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
