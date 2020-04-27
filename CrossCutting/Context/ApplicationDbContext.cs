using CrossCutting.Configuration;
using CrossCutting.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace CrossCutting.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRoleManager,string>
    {
        private readonly IHostingEnvironment _env;

        public ApplicationDbContext(
                    DbContextOptions<ApplicationDbContext> options,
                    IHostingEnvironment env) : base(options)
        {
            _env = env;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cofig = new ConfigurationBuilder().SetBasePath(_env.ContentRootPath)
            .AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(cofig.GetConnectionString("DefaultConnection"));
        }
    }
}
