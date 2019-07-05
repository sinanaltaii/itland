using EPiServer.Core;
using ITLand.Web.Features.Shared;

namespace ITLand.Web.Features.Start
{
	public class StartPageViewModelFactory : IViewModelFactory<StartPageViewModel>
	{
		public StartPageViewModel Create<TContent>(TContent content) where TContent : IContent
		{
			var model = CreateModel(content);
			return model;
		}

		public StartPageViewModel CreateModel(IContent contentType)
		{
			var startPage = contentType as StartPage;
			return new StartPageViewModel(startPage);
		}
	}
}