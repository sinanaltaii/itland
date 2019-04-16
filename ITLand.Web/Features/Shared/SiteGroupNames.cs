using System.ComponentModel.DataAnnotations;
using EPiServer.DataAnnotations;
using EPiServer.Security;

namespace ITLand.Web.Features.Shared
{
	[GroupDefinitions]
	public static class SiteGroupNames
	{
		[Display(Order = 10), RequiredAccess(AccessLevel.Edit)]
		public const string Specialized = "Specialized";

		[Display(Order = 20), RequiredAccess(AccessLevel.Edit)]
		public const string Common = "Common";

		[Display(Order = 30), RequiredAccess(AccessLevel.Edit)]
		public const string News = "News";
	}
}