using EPiServer.Core;
using ITLand.Web.Features.Shared;

namespace ITLand.Web.Features.Article
{
	public class ArticlePageViewModeFactory : IViewModelFactory<ArticlePageViewModel>
	{
		public ArticlePageViewModel Create<TContent>(TContent content) where TContent : IContent
		{
			var model = BuildViewModel(content);

			return model;
		}

		public ArticlePageViewModel BuildViewModel(IContent contentType)
		{
			var articlePage = contentType as ArticlePage;
			return new ArticlePageViewModel(articlePage?.Heading, articlePage?.Preamble, articlePage?.MainBody);
		}
	}
}