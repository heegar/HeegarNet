using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace EMR_OMS.web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/lorem/jquery").Include(
                      "~/assets/js/jquery.min.js"
                ));
            bundles.Add(new ScriptBundle("~/lorem/scripting").Include(
                      "~/assets/js/skel.min.js",
                //"~/assets/js/ie/html5shiv.js",
                      "~/assets/js/util.js",
                      "~/assets/js/jquery-ui.min.js",
                      "~/assets/js/main.js"));

            bundles.Add(new StyleBundle("~/lorem/css").Include(
                      "~/assets/css/heegar.css",
                      "~/assets/css/jquery-ui.min.css",
                      "~/assets/css/jquery-ui.structure.min.css",
                      "~/assets/css/jquery-ui.theme.min.css",
                      "~/assets/css/main.css")
                      .Include("~/assets/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // Add Custom View scripts here
            //bundles.Add(new ScriptBundle("~/bundles/Tickets").Include(
            //          "~/Scripts/Tickets/TicketsHome.js"
            //    ));
        }
    }
}