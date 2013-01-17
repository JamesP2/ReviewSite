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
                "GetTag",
                "Tag/{*id}",
                new { controller = "Home", action = "GetTag" }
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
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                Application_Error(new HttpException(404, Context.Response.StatusDescription));
            }
        }

        protected void Application_Error()
        {
            Application_Error(Server.GetLastError());

        }
        protected void Application_Error(Exception ex)
        {
            var controller = new HomeController();
            var view = controller.Error(ex);

            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var route = RouteTable.Routes.GetRouteData(httpContext);

            if (route == null) return;

            Server.ClearError();

            var controllerContext = new ControllerContext(new RequestContext(httpContext, route), controller);
            view.ExecuteResult(controllerContext);
        }
    }
}