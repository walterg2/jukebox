using Hangfire;
using Hangfire.SqlServer;
using Jukebox;
using Jukebox.Jobs;
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
                config.UseAutofacActivator(IoC.Autofac.Configuration);
            });

            //RecurringJob.AddOrUpdate<ScanMusic>(x => x.ScanFolder(@"C:\cdmp3"), "5 * * * *");
            BackgroundJob.Enqueue<ScanMusic>(x => x.PerformInitialScan(@"C:\cdmp3"));
        }
    }
}
