using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace ITLand.Web.Features.Start
{
	public class StartPageController : PageController<StartPage>
	{
		public ActionResult Index(StartPage currentPage)
		{
			var factory = new StartPageViewModelFactory();
			var model = factory.Create(currentPage);
			
			return View(model);
		}
	}
}