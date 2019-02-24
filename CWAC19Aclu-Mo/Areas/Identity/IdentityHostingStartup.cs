using System;
using CWAC19AcluMo.Areas.Identity.Data;
using CWAC19AcluMo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CWAC19AcluMo.Areas.Identity.IdentityHostingStartup))]
namespace CWAC19AcluMo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppUserContextConnection")));

                services.AddDefaultIdentity<AppUser>().AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppUserContext>();
            });
        }
    }
}