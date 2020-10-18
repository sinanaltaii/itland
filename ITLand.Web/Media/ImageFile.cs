using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace ITLand.Web.Media
{
	[ContentType(
		DisplayName = "Image File",
		GUID = "b6f21e28-2bfe-4a80-b97b-6c9f7dacaccd",
		Description = "Use to upload image files")]
	[MediaDescriptor(ExtensionString = "png,gif,jpg,jpeg")]
	public class ImageFile : ImageData
	{
		[CultureSpecific]
		[Editable(true)]
		public virtual string Description { get; set; }
	}
}
