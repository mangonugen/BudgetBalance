using System.Web;
using System.Web.Optimization;

namespace TransactionBalance
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                        "~/Scripts/jquery.mmenu.umd.all.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryAddon").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/iosOverlay.js"));

            bundles.Add(new ScriptBundle("~/bundles/spin").Include(
                        "~/Scripts/spin.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularAMD").Include(
                      "~/Scripts/angular-sanitize.js",
                      "~/Scripts/angular-route.js",
                      //"~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                      "~/Scripts/angularAMD.js"));

            //bundles.Add(new ScriptBundle("~/bundles/angularRoute").Include(                      
            //          "~/Scripts/angular-route.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.mmenu.all.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-submenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/require").Include(
                        "~/Scripts/require.js"));

            bundles.Add(new ScriptBundle("~/bundles/appConfig").Include(
                        "~/Scripts/appConfig.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxService").Include(
                        "~/Scripts/services/ajaxService.js"));

            bundles.Add(new ScriptBundle("~/bundles/accountsService").Include(
                        "~/Scripts/services/accountsService.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainService").Include(
                        "~/Scripts/services/mainService.js"));

            bundles.Add(new ScriptBundle("~/bundles/require1").Include(
                        "~/Scripts/require.js",
                        "~/Scripts/app.js"
                        ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
