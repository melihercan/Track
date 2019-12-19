using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: HostingStartup(typeof(Shared.Startup))]
namespace Shared
{
    public class Startup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //throw new NotImplementedException();
        }
    }
}
