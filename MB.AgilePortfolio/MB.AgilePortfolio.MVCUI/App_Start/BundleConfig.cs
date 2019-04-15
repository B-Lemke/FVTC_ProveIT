using System.Web;
using System.Web.Optimization;

namespace MB.AgilePortfolio.MVCUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/contact_me.js",
                      "~/Scripts/jqBootstrapValidation.js",
                      "~/Scripts/jquery.magnific-popup.js",
                      "~/Scripts/umd/popper.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/freelancer.js"

                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/languageBadge.css",
                      "~/Content/freelancer.css"
                      ));




        }
    }
}
