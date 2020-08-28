using System.Web.Optimization;

namespace StudentManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           bundles.Add(new StyleBundle("~/Content/css").Include(
                                       "~/Content/bootstrap.css"
                                     , "~/Content/site.css"
                                     , "~/Content/jquery.dataTables.min.css"
                                     , "~/Content/bootstrap-select.min.css"
                                       ));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-1.11.3.js"
                                        , "~/Scripts/jquery.dataTables.min.js"
                                        , "~/Scripts/bootstrap-3.3.6.js"
                                        , "~/Scripts/bootstrap-3.3.5.js"
                                        , "~/Scripts/bootstrap-3.3.7.js"
                                        , "~/Scripts/moment.min.js"
                                        , "~/Scripts/bootstrap-select.min.js"
                                        , "~/Scripts/jquery.validate*"

            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
        }
    }
}
