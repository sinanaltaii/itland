using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;
using IHttpDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;
using IMvcDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace ITLand.Web.Bootstrapping
{
	public class StructureMapDependencyResolver : IHttpDependencyResolver, IMvcDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapDependencyResolver(IContainer container)
		{
			_container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public object GetService(Type serviceType)
		{
			if (serviceType.IsInterface || serviceType.IsAbstract)
			{
				return GetInterfaceService(serviceType);
			}

			return GetConcreteService(serviceType);
		}

		private object GetConcreteService(Type serviceType)
		{
			return _container.GetInstance(serviceType);
		}

		private object GetInterfaceService(Type serviceType)
		{
			return _container.TryGetInstance(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _container.GetAllInstances(serviceType).Cast<object>();
		}

		public IDependencyScope BeginScope()
		{
			var child = _container.GetNestedContainer();
			return new StructureMapDependencyResolver(child);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		~StructureMapDependencyResolver()
		{
			Dispose(false);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			_container?.Dispose();
		}
	}
}