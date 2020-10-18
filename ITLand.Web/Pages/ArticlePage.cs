using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using ITLand.Web.Features.Shared;

namespace ITLand.Web.Pages
{
	[ContentType(DisplayName = "Article",
		GUID = "8f66bc01-f0d8-4372-ac20-a27ca7fea18f",
		GroupName = SiteGroupNames.News)]
	public class ArticlePage : PageData, IPage
	{
		[CultureSpecific]
		[Display(Order = 20)]
		public virtual string Heading { get; set; }

		[CultureSpecific]
		[Display(Order = 20)]
		[UIHint(UIHint.Textarea)]
		public virtual string Preamble { get; set; }

		[CultureSpecific]
		[Display(GroupName = SystemTabNames.Content, Order = 30)]
		public virtual XhtmlString MainBody { get; set; }
	}
}
