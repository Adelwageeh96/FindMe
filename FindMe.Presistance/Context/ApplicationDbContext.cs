using Castle.Core.Resource;
using FindMe.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Reflection;
using FindMe.Domain.Models;


namespace FindMe.Presistance.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Account");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Account");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Account");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Account");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Account");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Account");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Account");

            modelBuilder.HasDefaultSchema("FindMe");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<OrganizaitonJoinRequest> OrganizaitonJoinRequests { get; set; }
        public DbSet<PinnedPost> PinnedPosts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RecognitionRequest> RecognitionRequests { get; set; }
        public DbSet<RecognitionRequestResult> RecognitionRequestsResult { get; set;}
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserRelatives> UserRelatives { get; set; } 
 

    }
}
