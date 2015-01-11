using System.Web.Optimization;
using Raven.Client.Linq;

namespace Jukebox
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/js/vendor/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/js/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/js/vendor/bootstrap.js", 
                "~/js/vendor/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/js/vendor/angular/angular.js",
                "~/js/vendor/angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/jukebox").Include(
                "~/js/jukebox.js",
                "~/js/controllers/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
