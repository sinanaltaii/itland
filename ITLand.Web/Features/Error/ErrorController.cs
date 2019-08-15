using System.Web.Mvc;

namespace ITLand.Web.Features.Error
{
	public class ErrorController : Controller
	{
		public ActionResult NotFound()
		{
			Response.StatusCode = 404;
			return View();
		}
	}
}