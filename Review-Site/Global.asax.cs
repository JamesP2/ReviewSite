using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Review_Site.Controllers;

namespace Review_Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MonoWebFormViewEngine : WebFormViewEngine
    {
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath.Replace("~", ""));
        }
    }
    public class MonoRazorViewEngine : RazorViewEngine
    {
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath.Replace("~", ""));
        }
    }

    public class MvcApplication : HttpApplication
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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            // Put Mono-supporting view engines in the context
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MonoWebFormViewEngine());
            ViewEngines.Engines.Add(new MonoRazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            var controller = new HomeController();
            var result = controller.Error(Server.GetLastError());

            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var requestContext = new RequestContext(httpContext, RouteTable.Routes.GetRouteData(httpContext));
            var context = new ControllerContext(requestContext, controller);

            result.ExecuteResult(context);
        }
    }
}