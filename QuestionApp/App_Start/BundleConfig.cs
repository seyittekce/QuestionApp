using System.Web.Optimization;
namespace QuestionApp
{
    public class BundleConfig
    {
        // Paketleme hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301862 adresini ziyaret edin
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
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/Datatable").Include(
     "~/Scripts/jquery.dataTables.min.js",
       "~/Scripts/Datatable/dataTables.buttons.min.js",
         "~/Scripts/Datatable/buttons.flash.min.js",
           "~/Scripts/Datatable/pdfmake.min.js",
             "~/Scripts/Datatable/buttons.html5.min.js",
               "~/Scripts/Datatable/dataTables.buttons.min.js",
               "~/Scripts/Datatable/jszip.min.js",
               "~/Scripts/Datatable/vfs_fonts.js"
    ));
            bundles.Add(new ScriptBundle("~/bundles/models")
                            .Include("~/Scripts/vm.department.js")
                            .Include("~/Scripts/vm.question.js")
                            .Include("~/Scripts/vm.responselist.js")
                            .Include("~/Scripts/vm.survey.js")
                            .Include("~/Scripts/vm.surveylist.js"));
        }
    }
}
