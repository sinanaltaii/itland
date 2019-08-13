using System.Web;
using System.Web.Routing;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ITLand.Tests.IntegrationsTests.RoutingTests
{
	public class RouteTests
	{
		[Fact]
		public void GivenConfiguredRoutes_WhenGettingNonExistingController_ThenRouteFails()
		{
			AssertRouteFail("~/NonExistingController");
		}

		[Fact]
		public void GivenConfiguredRoutes_WhenGettingNonExistingAction_ThenRouteFails()
		{
			AssertRouteFail("~/Error/NonExistingAction");
		}

		private static void AssertRouteFail(string url)
		{
			var routes = new RouteCollection();
			var result = routes.GetRouteData(MakeHttpContextFake(url));
			(result?.Route == null).ShouldBe(true);
		}

		private static HttpContextBase MakeHttpContextFake(string targetUrl = null, string httpMethod = "GET")
		{
			var request = Substitute.For<HttpRequestBase>();
			request.AppRelativeCurrentExecutionFilePath.Returns(targetUrl);
			request.HttpMethod.Returns(httpMethod);

			var response = Substitute.For<HttpResponseBase>();
			response.ApplyAppPathModifier(Arg.Any<string>()).Returns("");

			var context = Substitute.For<HttpContextBase>();
			context.Request.Returns(request);
			context.Response.Returns(response);

			return context;
		}
	}
}
