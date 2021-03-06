﻿using System.Web;
using System.Web.Optimization;

namespace BistroDriveWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/jquery.touchSwipe.min.js",
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/carousel").Include("~/Scripts/jcarousel.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app.js"));
            bundles.Add(new ScriptBundle("~/bundles/touchSwipe").Include("~/Scripts/jquery.touchSwipe.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/fullpage").Include("~/Scripts/jquery.fullPage.js"));
            bundles.Add(new ScriptBundle("~/bundles/easings").Include("~/Scripts/jquery.easings.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include("~/Scripts/jquery.slimscroll.min.js"));


            bundles.Add(new StyleBundle("~/Content/landing").Include("~/Content/Landing.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/jquery.fullPage.css",
                      "~/Content/site.css",
                      "~/Content/Landing.less",
                      "~/Content/Forms.less",
                      "~/Content/Orders.less",
                      "~/Content/UsersList.less",
                      "~/Content/Dish.less"));

            
            
        }
    }
}
