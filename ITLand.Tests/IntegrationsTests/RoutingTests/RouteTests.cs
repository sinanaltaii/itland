using System;
using System.Web;
using System.Web.Routing;
using ITLand.Web;
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

		[Fact]
		public void GivenConfiguredRoutes_WhenGettingNonExistingPage_Then404PageIsReturned()
		{
			AssertRouteMatch("~/Error/NotFound", "Error", "NotFound");
		}

		[Fact]
		public void GivenConfiguredRoutes_WhenInternalServerErrorOccurs_Then500IsReturned()
		{
			AssertRouteMatch("~/Error/InternalServerError", "Error", "InternalServerError");
		}

		private static void AssertRouteMatch(string url, string controller, string action)
		{
			var routes = new RouteCollection();
			RouteConfig.RegisterRoutes(routes);
			var routeData = routes.GetRouteData(MakeHttpContextFake(url));
			routeData.ShouldNotBeNull();
			IncomingRouteResultValid(routeData, controller, action);
		}

		private static bool IncomingRouteResultValid(RouteData routeData, string controller, string action, object propertySet = null)
		{
			var result = Compare(routeData.Values["controller"], controller) && Compare(routeData.Values["action"], action);

			if (propertySet != null)
			{
				var propertyInfos = propertySet.GetType().GetProperties();
				foreach (var propertyInfo in propertyInfos)
				{
					if (!(routeData.Values.ContainsKey(propertyInfo.Name) && Compare(routeData.Values[propertyInfo.Name], propertyInfo.GetValue(propertySet, null))))
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		private static bool Compare(object v1, object v2)
		{
			return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
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
