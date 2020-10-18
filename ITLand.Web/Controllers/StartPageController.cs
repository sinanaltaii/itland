using System.Web.Mvc;
using EPiServer.Web.Mvc;
using ITLand.Web.Features.Start;
using ITLand.Web.Pages;

namespace ITLand.Web.Controllers
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
