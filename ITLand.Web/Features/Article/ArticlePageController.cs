using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace ITLand.Web.Features.Article
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