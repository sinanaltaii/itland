using System.Web.Http;
using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace ITLand.Web.Bootstrapping
{
	[InitializableModule, ModuleDependency(typeof(ServiceContainerInitialization))]
	public class ContianerInitialization : IConfigurableModule
	{
		public void ConfigureContainer(ServiceConfigurationContext context)
		{
			context.Services.AddSingleton(c => context);
			new AppBootstrapper().RegisterDependencies(context.Services);
			var resolver = new StructureMapDependencyResolver(context.StructureMap());
			DependencyResolver.SetResolver(resolver);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}

		public void Initialize(InitializationEngine context) { }

		public void Uninitialize(InitializationEngine context) { }

	}
}
