using CrossCutting.Model;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {

        public UserDbContext(DbContextOptions<UserDbContext>options) : base(options)
        {

        }
        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

        public DbSet<ApplicationUser> Usuarios { get; set; }

        //public DbSet<IdentityUserRole<string>> IdentityUserRole { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasKey(c => new { c.Id });
    

            base.OnModelCreating(modelBuilder);
            
        }
  
    }
}
