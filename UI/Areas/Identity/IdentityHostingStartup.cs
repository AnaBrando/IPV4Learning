using CrossCutting.Model;
using Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;

[assembly: HostingStartup(typeof(UI.Areas.Identity.IdentityHostingStartup))]
namespace UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                
            });
        }
    }
}
