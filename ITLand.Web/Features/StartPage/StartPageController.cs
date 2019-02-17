using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace ITLand.Web.Features.StartPage
{
	public class StartPageController : PageController<StartPageType>
	{
		public ActionResult Index(StartPageType currentPage)
		{
			var model = new StartPageViewModel(currentPage.Heading, currentPage.MainBody);
			return View(model);
		}
	}
}