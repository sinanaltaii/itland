using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace ITLand.Web.Features.Article
{
	public class ArticlePageController : PageController<ArticlePage>
	{
		public ActionResult Index(ArticlePage currentPage)
		{
			var factory = new ArticlePageViewModeFactory();
			var model = factory.Create(currentPage);
			return View(model);
		}
	}
}