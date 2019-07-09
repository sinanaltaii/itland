using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace ITLand.Web.Features.Start
{
	public class StartPageController : PageController<StartPage>
	{
		private readonly StartPageViewModelFactory _factory;

		public StartPageController(StartPageViewModelFactory factory)
		{
			_factory = factory;
		}

		public ActionResult Index(StartPage currentPage)
		{
			var model = _factory.Create(currentPage);
			return View(model);
		}
	}
}