using EPiServer.Core;

namespace ITLand.Web.Features.Shared
{
	public interface IViewModelFactory<out T> where T : class
	{
		T Create<TContent>(TContent content) where TContent : IContent;
		T CreateModel(IContent contentType);
	}
}
