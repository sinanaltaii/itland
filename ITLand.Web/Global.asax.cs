using System.Web.Mvc;
using System.Web.Routing;

namespace ITLand.Web
{
	public class Global : EPiServer.Global
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.MapRoute(
				"Error/NotFound",
				"Error/NotFound",
				new { controller = "Error", action = "NotFound" });
			RouteTable.Routes.MapRoute(
				"Error/InternalServerError",
				"Error/InternalServerError",
				new { controller = "Error", action = "InternalServerError" }
			);
		}
	}
}