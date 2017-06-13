﻿using System.Web;
using System.Web.Optimization;

namespace MaaseShooei
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/DataTables-1.10.10/media/js/jquery.dataTables.js",
                        "~/Content/bootstrap/js/bootstrap.js",
                        "~/Scripts/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js",
                        "~/Scripts/MdBootstrapPersianDateTimePicker/jalaali.js"));

            // bundles.Add(new ScriptBundle("~/bundles/jquery1").Include("~/Scripts/jquery.js"));
            // "~/Scripts/jquery-182.js",    
            //"~/Scripts/jquery.js",
            //bundles.Add(new ScriptBundle("~/bundles/dtResponsive").Include(
            //            "~/Scripts/dtResponsive.js"));
            //"~/Scripts/jquery-{version}.js",

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //            "~/Content/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/DataTables-1.10.10/media/css/jquery.dataTables.css",
                "~/Content/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css"));
            //"~/Content/site.css",
            //bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include("~/Content/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
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
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/themes/base/jquery-ui.css"
                        ));




            //"~/Content/themes/base/jquery-ui.css",














            //    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //                "~/Scripts/jquery-{version}.js"));

            //    bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //                "~/Scripts/jquery-ui-{version}.js"));

            //    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //                "~/Scripts/jquery.unobtrusive*",
            //                "~/Scripts/jquery.validate*"));

            //    // Use the development version of Modernizr to develop with and learn from. Then, when you're
            //    // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //    bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //                "~/Scripts/modernizr-*"));

            //    bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //    bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //                "~/Content/themes/base/jquery.ui.core.css",
            //                "~/Content/themes/base/jquery.ui.resizable.css",
            //                "~/Content/themes/base/jquery.ui.selectable.css",
            //                "~/Content/themes/base/jquery.ui.accordion.css",
            //                "~/Content/themes/base/jquery.ui.autocomplete.css",
            //                "~/Content/themes/base/jquery.ui.button.css",
            //                "~/Content/themes/base/jquery.ui.dialog.css",
            //                "~/Content/themes/base/jquery.ui.slider.css",
            //                "~/Content/themes/base/jquery.ui.tabs.css",
            //                "~/Content/themes/base/jquery.ui.datepicker.css",
            //                "~/Content/themes/base/jquery.ui.progressbar.css",
            //                "~/Content/themes/base/jquery.ui.theme.css"));


            //BundleTable.EnableOptimizations = true;
        }
    }
}