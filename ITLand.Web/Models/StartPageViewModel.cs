using EPiServer.Core;
using ITLand.Web.Pages;

namespace ITLand.Web.Models
{
	public class StartPageViewModel
	{
		public string Heading { get; }
		public string Preamble { get; set; }
		public XhtmlString MainBody { get; }
		public ContentArea MainContentArea { get; set; }

		public StartPageViewModel(StartPage startPage)
		{
			Heading = startPage.Heading;
			Preamble = startPage.Preamble;
			MainBody = startPage.MainBody;
			MainContentArea = startPage.MainContentArea;
		}
	}
}
