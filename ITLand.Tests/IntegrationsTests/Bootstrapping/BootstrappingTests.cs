using EPiServer.ServiceLocation.Internal;
using StructureMap;
using System.Linq;
using System.Web.Mvc;
using Xunit;
using System;
using Global = ITLand.Web.Global;
using Shouldly;

namespace ITLand.Tests.IntegrationsTests.Bootstrapping
{
	public class BootstrappingTests
	{
		[Fact]
		public void GivenRegisteredDependencies_WhenResolvingControllers_ThenAllControllersCanBeResolved()
		{
			var controllersTypes = typeof(Global).Assembly.GetTypes()
				.Where(t => t.IsAbstract == false)
				.Where(t => typeof(Controller).IsAssignableFrom(t))
				.Where(t => t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
			var sut = CreateStructureMapConfiguration();
			var registeredControllerTypes = sut.GetAllInstances<Controller>().Select(c => c.GetType());
			controllersTypes.ShouldBe(registeredControllerTypes);
		}

		private static StructureMapConfiguration CreateStructureMapConfiguration()
		{
			var container = new Container();
			container.Configure(c => c.Scan(a =>
			{
				a.AssemblyContainingType<Global>();
				a.AddAllTypesOf<Controller>();
			}));
			var structureMapConfiguration = new StructureMapConfiguration(container);
			return structureMapConfiguration;
		}
	}
}
