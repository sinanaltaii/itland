using EPiServer.ServiceLocation;
using ITLand.Web.Features.Article;
using ITLand.Web.Features.Start;

namespace ITLand.Web.Bootstrapping
{
	public class AppBootstrapper
	{
		public void RegisterDependencies(IServiceConfigurationProvider services)
		{
			services.AddScoped<StartPageViewModelFactory>();
			services.AddScoped<ArticlePageViewModeFactory>();
		}
	}
}