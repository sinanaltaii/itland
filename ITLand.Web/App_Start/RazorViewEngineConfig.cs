using System.Linq;
using System.Web.Mvc;

namespace ITLand.Web
{
	public class RazorViewEngineConfig
	{
		public static void RegisterViewLocations(RazorViewEngine razorViewEngine)
		{
			razorViewEngine.ViewLocationFormats = razorViewEngine.ViewLocationFormats.Concat(new[]
			{
				"~/Views/Shared/{1}/{0}.cshtml"
			}).ToArray();
		}

		public static void RegisterPartialViewLocations(RazorViewEngine razorViewEngine)
		{
			razorViewEngine.PartialViewLocationFormats = razorViewEngine.PartialViewLocationFormats.Concat(new[]
			{
				"~/Views/Shared/{1}/{0}.cshtml"
			}).ToArray();
		}
	}
}