﻿using System.Web;
using System.Web.Optimization;

namespace CherwellTest
{
    /// <summary>Represents the bundle configuration for the web site.</summary>
    public class BundleConfig
    {
        /// <summary>Registers the predefined collections, or bundles, of scripts and stylesheets.</summary>
        /// <param name="bundles">Contains and manages the set of registered bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css","~/Content/site.css"));
        }
    }
}