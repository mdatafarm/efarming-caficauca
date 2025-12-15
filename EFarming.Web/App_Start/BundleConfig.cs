using System.Web;
using System.Web.Optimization;

namespace EFarming.Web
{
    /// <summary>
    /// Bundle config
    /// </summary>
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/docs").Include(
                       "~/Scripts/docs.js"));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap*",
                        "~/Scripts/validation*"));

            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                        "~/Scripts/ie-emulation-modes-warning.js",
                        "~/Scripts/ie10-viewport-bug-workaround.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                                    "~/Scripts/knockout.js",
                                    "~/Scripts/knockout.validation.js",
                                    "~/Scripts/bootstrap.knockout.validation.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css", 
                        "~/Content/Site.css",
                        "~/Content/Indicators.css"));

            bundles.Add(new StyleBundle("~/Content/Font").Include(
                "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                "~/Content/login.css"));

            bundles.Add(new StyleBundle("~/Content/Home").Include(
                "~/Content/dashboard.css",
                "~/Content/home.css",
                "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap*"));


            bundles.Add(new StyleBundle("~/Content/FileUpload").Include(
                "~/Content/FileUpload/css/jquery.fileupload.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/jquery-ui.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));



            BundleTable.EnableOptimizations = true;
        }
    }
}