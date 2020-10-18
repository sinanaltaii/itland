using System.Net;
using System.Web.Mvc;

namespace ITLand.Web.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult NotFound()
		{
			Response.StatusCode = (int)HttpStatusCode.NotFound;
			return View();
		}

		public ActionResult InternalServerError()
		{
			Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return View();
		}
	}
}
