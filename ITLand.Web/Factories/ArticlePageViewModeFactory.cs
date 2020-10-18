using EPiServer.Core;
using ITLand.Web.Features.Shared;
using ITLand.Web.Models;
using ITLand.Web.Pages;

namespace ITLand.Web.Features.Article
{
    public class ArticlePageViewModeFactory : IViewModelFactory<ArticlePageViewModel>
	{
		public ArticlePageViewModel Create<TContent>(TContent content) where TContent : IContent
		{
			var model = CreateModel(content);
			return model;
		}

		public ArticlePageViewModel CreateModel(IContent contentType)
		{
			var articlePage = contentType as ArticlePage;
			return new ArticlePageViewModel(articlePage?.Heading, articlePage?.Preamble, articlePage?.MainBody);
		}
	}
}
