using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Review_Site.Models;
using Review_Site.Core;

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

            ReviewSiteEntities db = new ReviewSiteEntities();
            if (!db.Users.Select(x => x.Username).Contains("admin"))
            {
                //Create a test user!
                User u = new User
                {
                    ID = Guid.NewGuid(),
                    Username = "admin",
                    Password = PasswordHashing.GetHash("password"),
                    FirstName = "James",
                    LastName = "Phillips",
                };

                db.Users.AddObject(u);
                db.SaveChanges();
            }

            if (!db.Colors.Any())
            {
                //No colours. add some.
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Light Green", Value = "8AA359" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Purple", Value = "9900FF" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Blue", Value = "0B76C2" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Light Orange", Value = "F3B32B" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Light Red", Value = "FF5250" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Pink", Value = "FF36FF" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Light Blue", Value = "1582B3" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Light Brown", Value = "DA9349" });
                db.Colors.AddObject(new Color { ID = Guid.NewGuid(), Name = "Grey", Value = "858786" });
                db.SaveChanges();
            }
        }
    }
}