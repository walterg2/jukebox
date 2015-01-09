using Hangfire;
using Hangfire.SqlServer;
using Jukebox;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Jukebox
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage(@"Server=.\SQLExpress;Database=JukeboxJobs;User Id=sa; Password=northwoods;");
                config.UseServer();
            });
        }
    }
}
