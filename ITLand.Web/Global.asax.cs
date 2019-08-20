using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITLand.Web
{
	public class Global : EPiServer.Global
	{
		protected void Application_Start()
		{
			var razorViewEngine = ViewEngines.Engines.OfType<RazorViewEngine>().First();
			RazorViewEngineConfig.RegisterViewLocations(razorViewEngine);
			RazorViewEngineConfig.RegisterPartialViewLocations(razorViewEngine);
			AreaRegistration.RegisterAllAreas();
		}

		protected override void RegisterRoutes(RouteCollection routes)
		{
			base.RegisterRoutes(routes);
			RouteConfig.RegisterRoutes(routes);
		}
	}
}