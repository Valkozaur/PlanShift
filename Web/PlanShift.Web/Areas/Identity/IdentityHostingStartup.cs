using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PlanShift.Web.Areas.Identity.IdentityHostingStartup))]

namespace PlanShift.Web.Areas.Identity
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