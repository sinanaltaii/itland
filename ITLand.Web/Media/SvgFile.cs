using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;

namespace ITLand.Web.Media
{
	[ContentType(
		DisplayName = "Svg File",
		GUID = "bf1f53ab-2534-4f74-bb1f-928a95816bcf",
		Description = "Use to upload svg file")]
	[MediaDescriptor(ExtensionString = "svg")]
	public class SvgFile : MediaData
	{
		public override Blob Thumbnail => base.BinaryData;
	}
}
