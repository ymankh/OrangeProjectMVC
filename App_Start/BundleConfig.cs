using System.Web.Optimization;

namespace OrangeProjectMVC
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
            //< link href = "" rel = "stylesheet" type = "text/css" >



            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/bootstrap.css",
              "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/dashbored").Include(
                "~/Content/css/sb-admin-2.min.css",
                "~/Scripts/vendor/fontawesome-free/css/all.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/dashbored").Include(
                "~/Scripts/vendor/jquery/jquery.min.js",
                "~/Scripts/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Scripts/vendor/jquery-easing/jquery.easing.min.js",
                "~/Scripts/js/sb-admin-2.min.js",
                "~/Scripts/vendor/chart.js/Chart.min.js"));



            bundles.Add(new StyleBundle("~/Content/userCycle").Include(
            "~/Content/bootstrap.min.css",
            "~/Content/site.css",
            "~/assets/css/animate.css",
            "~/assets/css/leaflet-routing-machine.css",
            "~/assets/css/leaflet.css",
            "~/assets/css/magnific-popup.css",
            "~/assets/css/style.css",
            "~/assets/css/swiper-bundle.min.css",
            "~/assets/css/customStyle.css"
            ));

            bundles.Add(new StyleBundle("~/flip/css").Include(
                "~/assets/flip/flip.min.css"
            ));

            bundles.Add(new Bundle("~/flip/js").Include(
                "~/assets/flip/flip.js"
            ));


            bundles.Add(new Bundle("~/bundles/userCycle").Include(
                "~/assets/js/animate.js",
                "~/assets/js/bootstrap.bundle.min.js",
                "~/assets/js/countdown.js",
                "~/assets/js/jquery-3.7.0.min.js",
                "~/assets/js/jquery.magnific-popup.min.js",
                "~/assets/js/jquery.progressScroll.min.js",
                "~/assets/js/lazy.image.js",
                "~/assets/js/leaflet-routing-machine.min.js",
                "~/assets/js/leaflet-scripts.js",
                "~/assets/js/leaflet.js",
                "~/assets/js/script.js",
                "~/assets/js/swiper-bundle.min.js",
                "~/assets/js/wow.min.js"
                ));

        }
    }
}
