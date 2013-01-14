using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Review_Site.Models;
using Review_Site.Core;
using System.Net;
using Review_Site.Controllers;

namespace Review_Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GetResource",
                "Resource/{id}",
                new { controller = "Home", action = "GetResource" }
            );

            routes.MapRoute(
                "GetArticle",
                "Article/{id}",
                new { controller = "Home", action = "GetArticle" }
            );

            routes.MapRoute(
                "GetGrid",
                "Grid/{id}",
                new { controller = "Home", action = "GetGrid" }
            );

            routes.MapRoute(
                "GetCategory",
                "Category/{*id}",
                new { controller = "Home", action = "GetCategory" }
            );

            routes.MapRoute(
                "About",
                "About/",
                new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            SiteContext db = new SiteContext();




            if (!db.Categories.Any(x => x.Title == "Home"))
            {
                //Add a system category to serve as the base for the homepage
                db.Categories.Add(new Category
                {
                    ID = Guid.NewGuid(),
                    Title = "Home",
                    IsSystemCategory = true,
                    Color = db.Colors.Single(x => x.Name == "Grey")
                }
                );

                db.SaveChanges();

            }
        }

        protected void Application_Error()
        {
            HomeController controller = new HomeController();
            controller.Error(Server.GetLastError());
        }
    }
}