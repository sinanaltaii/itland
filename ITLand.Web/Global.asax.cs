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
				"Error",
				"Error",
				new { controller = "Error", action = "NotFound" });
		}
	}
}