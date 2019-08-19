using System.Web.Mvc;
using System.Web.Routing;

namespace ITLand.Web
{
	public class Global : EPiServer.Global
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
		}

		protected override void RegisterRoutes(RouteCollection routes)
		{
			base.RegisterRoutes(routes);
			RouteConfig.RegisterRoutes(routes);
		}
	}
}