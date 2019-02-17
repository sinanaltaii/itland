using EPiServer.Core;

namespace ITLand.Web.Features.StartPage
{
	public class StartPageViewModel
	{
		public string Heading { get; }
		public XhtmlString MainBody { get; }

		public StartPageViewModel(string heading, XhtmlString mainBody)
		{
			Heading = heading;
			MainBody = mainBody;
		}
	}
}