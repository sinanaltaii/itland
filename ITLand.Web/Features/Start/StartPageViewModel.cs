using EPiServer.Core;

namespace ITLand.Web.Features.Start
{
	public class StartPageViewModel
	{
		public string Heading { get; }
		public XhtmlString MainBody { get; }
		public ContentArea MainContentArea { get; set; }

		public StartPageViewModel(StartPage startPage)
		{
			Heading = startPage.Heading;
			MainBody = startPage.MainBody;
			MainContentArea = startPage.MainContentArea;
		}
	}
}