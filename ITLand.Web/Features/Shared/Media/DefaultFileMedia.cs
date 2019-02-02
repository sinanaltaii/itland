using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace ITLand.Web.Features.Shared.Media
{
	[ContentType(
		DisplayName = "Default Media File", 
		GUID = "da3448ca-53dd-4337-aa1c-672f4917a5f7",
		Description = "This is a default media file")]
	public class DefaultFileMedia : MediaData
	{
	}
}