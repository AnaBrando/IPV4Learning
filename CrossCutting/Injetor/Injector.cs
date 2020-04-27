using Domain.Interfaces.Application;
using Domain.Interfaces.IPSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Service.Email;
using Service.IP;
using Service.Usuario;

namespace CrossCutting.Injetor
{
    public class Injector
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            { 
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<PasswordHasherOptions>(options =>
            options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

            services.AddTransient<EmailSender>(x => new EmailSender("smtp.gmail.com", 587, true, "akiramon6669@gmail.com", "Ichigobleach0"));                   
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IIPService, IPService>();
           

        }

    }
}
