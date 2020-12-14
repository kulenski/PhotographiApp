using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PhotographiApp.Web.Areas.Identity.IdentityHostingStartup))]

namespace PhotographiApp.Web.Areas.Identity
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
