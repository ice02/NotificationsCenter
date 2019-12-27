using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NotificationCenter.Areas.Identity.IdentityHostingStartup))]
namespace NotificationCenter.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}