using System.Web.Mvc;
using EPiServer.Web.Mvc;
using ITLand.Web.Features.Article;
using ITLand.Web.Pages;

namespace ITLand.Web.Controllers
{
	public class ArticlePageController : PageController<ArticlePage>
	{
		private readonly ArticlePageViewModeFactory _factory;

		public ArticlePageController(ArticlePageViewModeFactory factory)
		{
			_factory = factory;
		}

		public ActionResult Index(ArticlePage currentPage)
		{
			var model = _factory.Create(currentPage);
			return View(model);
		}
	}
}
