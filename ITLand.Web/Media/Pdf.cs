using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace ITLand.Web.Media
{
	[ContentType(DisplayName = "Pdf", GUID = "12b8a266-9289-4ece-90ae-986b5b35d1f3")]
	[MediaDescriptor(ExtensionString = "pdf")]
	public class Pdf : MediaData
	{
	}
}
