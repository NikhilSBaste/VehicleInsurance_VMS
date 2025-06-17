using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleInsuranceProject.Repository.Models;

namespace VehicleInsuranceProject.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the relationship between Customer and IdentityUser
            builder.Entity<Customer>()
                .HasOne(c => c.IdentityUser)
                .WithMany()
                .HasForeignKey(c => c.IdentityUserId)
                .IsRequired();
        }
    }
}