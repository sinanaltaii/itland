using System.Web.Mvc;
using System.Web.Routing;

namespace ITLand.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute("Error/NotFound",
				"Error/NotFound",
				new { controller = "Error", action = "NotFound" });

			routes.MapRoute(
				"Error/InternalServerError",
				"Error/InternalServerError",
				new { controller = "Error", action = "InternalServerError" });
		}
	}
}
