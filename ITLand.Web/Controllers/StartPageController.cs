using System.Web.Mvc;
using EPiServer.Web.Mvc;
using ITLand.Web.Models.Pages;

namespace ITLand.Web.Controllers
{
	public class StartPageController : PageController<StartPageType>
	{
		public ActionResult Index(StartPageType currentPage)
		{

			return View(currentPage);
		}
	}
}