using System.Web.Mvc;
using System.Web.Routing;

namespace QuestionApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "StudentResult",
                url: "{controller}/{action}/{LibraryiD}/{ClassID}",
                defaults: new { controller = "Classes", action = "StudentResult", LibraryiD = UrlParameter.Optional, ClassID = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Exam Page",
              url: "{controller}/{action}/{LibraryID}/{QuestionID}",
              defaults: new { controller = "Students", action = "Exam", LibraryID = UrlParameter.Optional, QuestionID = UrlParameter.Optional }
          );

        }
    }
}
