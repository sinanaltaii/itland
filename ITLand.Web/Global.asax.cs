using System.Web.Mvc;

namespace ITLand.Web
{
	public class Global : EPiServer.Global
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}