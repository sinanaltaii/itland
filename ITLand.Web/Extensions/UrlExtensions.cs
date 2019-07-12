using System;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace ITLand.Web.Extensions
{
	public static class UrlExtensions
	{
		public static string ExternalUrlFromReference(this PageReference p)
		{
			var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
			var page = loader.Get<PageData>(p);
			var pageUrlBuilder = new UrlBuilder(page.LinkURL);
			EPiServer.Global.UrlRewriteProvider.ConvertToExternal(pageUrlBuilder, page.LinkURL, UTF8Encoding.UTF8);
			var pageUrl = pageUrlBuilder.ToString();

			var uriBuilder = new UriBuilder(EPiServer.Web.SiteDefinition.Current.SiteUrl);
			uriBuilder.Path = pageUrl;

			return uriBuilder.Uri.AbsoluteUri;
		}
	}
}