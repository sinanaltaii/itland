using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace ITLand.Web.Features.StartPage
{
	[ContentType(
		DisplayName = "Start page",
		GUID = "1deb2cf0-b5a5-4a4c-a19d-f8ea8eeaba96",
		GroupName = SiteGroupNames.Specialized,
		Description = "The home page for a website with an area for blocks and partial pages.")]
	public class StartPageType : PageData
	{
		[CultureSpecific]
		[Display(Name = "Start page heading", Description = "Heading",
			GroupName = SystemTabNames.Content,
			Order = 10)]
		public virtual string Heading { get; set; }

		[CultureSpecific]
		[Display(
			Name = "Main body",
			Description = "The main body containing content, text and etc.",
			GroupName = SystemTabNames.Content,
			Order = 20)]
		public virtual XhtmlString MainBody { get; set; }
	}
}