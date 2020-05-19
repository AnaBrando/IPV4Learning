using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetworkLearning.Middleware;
using Service.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Infra.Repository;
using Domain;
using CrossCutting.Injetor;
using Domain.Interfaces.Repository;
using Infra.Context;
using CrossCutting.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Infra.Repository.Helper;
using System;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //registrando os serviços
            Injector.RegistrarServicos(services);

            //localhost
            //var connection = Configuration.GetConnectionString("DefaultConnection");
            //var b = Environment.GetEnvironmentVariables();
            //var c = Environment.GetEnvironmentVariable("https://+:443;http://+:80");

            //Setar o contexto da aplicacao
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //setar o contexto da identidade do usuário

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedPhoneNumber = false;
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;
            })
           .AddDefaultUI(UIFramework.Bootstrap4)
           .AddEntityFrameworkStores<UserDbContext>()
           .AddDefaultTokenProviders();



            services.AddTransient<IEmailSender>(x => new EmailSender("smtp.gmail.com", 587, true, "akiramon6669@gmail.com", "Ichigobleach0"));

            var configure = services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IIPRepository, IPRepository>();
            services.AddTransient<IProfessorRepository, ProfessorRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseNodeModules(env.ContentRootPath);
            app.UseAuthentication();
            app.UseWebSockets();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        
        }
    }
}
