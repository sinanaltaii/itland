using EPiServer.Core;

namespace ITLand.Web.Features.Article
{
	public class ArticlePageViewModel
	{
		public string Heading { get; }
		public string Preamble { get; }
		public XhtmlString MainBody { get; set; }

		public ArticlePageViewModel(string heading, string preamble, XhtmlString mainBody)
		{
			Heading = heading;
			Preamble = preamble;
			MainBody = mainBody;
		}
	}
}