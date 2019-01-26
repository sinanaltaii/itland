using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace ITLand.Web.Editor
{
	[EditorDescriptorRegistration(TargetType = typeof(CategoryList))]
	public class HideCategoryListEditorDescriptor : EditorDescriptor
	{
		public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
		{
			base.ModifyMetadata(metadata, attributes);
			if (metadata.PropertyName == "icategorizable_category")
			{
				metadata.ShowForEdit = false;
			}
		}
	}
}