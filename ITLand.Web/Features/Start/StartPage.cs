using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using ITLand.Web.Features.Shared;

namespace ITLand.Web.Features.Start
{
	[ContentType(
		DisplayName = "Start page",
		GUID = "1deb2cf0-b5a5-4a4c-a19d-f8ea8eeaba96",
		GroupName = SiteGroupNames.Specialized)]
	public class StartPage : PageData, IPage
	{
		[CultureSpecific]
		[Display(Name = "Start page heading", Description = "Heading",
			GroupName = SystemTabNames.Content,
			Order = 10)]
		public virtual string Heading { get; set; }

		[CultureSpecific]
		[Display(Order = 20)]
		[UIHint(UIHint.Textarea)]
		public virtual string Preamble { get; set; }

		[CultureSpecific]
		[Display(
			Name = "Main body",
			Description = "The main body containing content, text and etc.",
			GroupName = SystemTabNames.Content,
			Order = 30)]
		public virtual XhtmlString MainBody { get; set; }
	}
}