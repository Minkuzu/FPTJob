using FPTJob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTJob.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<JobApplication>()
            //    .HasOne(ja => ja.JobListing)
            //    .WithMany(jl => jl.JobApplications)
            //    .HasForeignKey(ja => ja.JobListingId)
            //    .IsRequired();
            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>
                (
                entity =>
                {
                    entity.ToTable(name: "User");
                }
                );
            builder.Entity<IdentityRole>
                (
                entity =>
                {
                    entity.ToTable(name: "Role");
                }
                );
            builder.Entity<IdentityUserRole<string>>
                (
                entity =>
                {
                    entity.ToTable(name: "UserRoles");
                }
                );
            builder.Entity<IdentityUserClaim<string>>
                (
                entity =>
                {
                    entity.ToTable(name: "UserClaims");
                }
                );
            builder.Entity<IdentityUserLogin<string>>
                (
                entity =>
                {
                    entity.ToTable(name: "UserLogins");
                }
                );
            builder.Entity<IdentityRoleClaim<string>>
                (
                entity =>
                {
                    entity.ToTable(name: "RoleClaims");
                }
                );
            builder.Entity<IdentityUserToken<string>>
                (
                entity =>
                {
                    entity.ToTable(name: "UserTokens");
                }
                );
            builder.Entity<Employer>(
                entity =>
                {
                    entity.ToTable(name: "Employer");
                }
                );
            builder.Entity<JobSeeker>(
                entity =>
                {
                    entity.ToTable(name: "JobSeeker");
                }
                );
        }
    }
}
