using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using ITLand.Web;
using Shouldly;
using Xunit;

namespace ITLand.Tests.IntegrationsTests.EpiserverContentTypes
{
	public class EpiserverContentTypesTests
	{
		[Fact]
		public void AssertUniqueGuidsOnPageTypes()
		{
			var webAssembly = GetWebProjectAssembly();
			var typesDerivingFromPageData = FindAllDerivedTypes<PageData>(webAssembly);
			var contentTypeGuids = GetContentTypeGuids(typesDerivingFromPageData);
			contentTypeGuids.Distinct().Count().ShouldBe(contentTypeGuids.Count);
		}

		[Fact]
		public void AssertUniqueGuidsOnBlocks()
		{
			var webAssembly = GetWebProjectAssembly();
			var typesDerivingFromBlockData = FindAllDerivedTypes<BlockData>(webAssembly);
			var contentTypesGuids = GetContentTypeGuids(typesDerivingFromBlockData);
			contentTypesGuids.Distinct().Count().ShouldBe(contentTypesGuids.Count);
		}

		[Fact]
		public void AssertUniqueGuidsOnMediaDataFiles()
		{
			var webAssembly = GetWebProjectAssembly();
			var typesDerivingFromMediaData = FindAllDerivedTypes<MediaData>(webAssembly);
			var contentTypeGuids = GetContentTypeGuids(typesDerivingFromMediaData);
			contentTypeGuids.Distinct().Count().ShouldBe(contentTypeGuids.Count);
		}

		private static Assembly GetWebProjectAssembly()
		{
			var webAssembly = Assembly.GetAssembly(typeof(Global));
			return webAssembly;
		}

		private static IEnumerable<Type> FindAllDerivedTypes<T>(Assembly assembly)
		{
			var baseType = typeof(T);
			var derivedTypes = assembly.GetTypes()
				.Where(t => t != baseType && baseType.IsAssignableFrom(t)).ToList();
			return derivedTypes;
		}

		private static IList<string> GetContentTypeGuids(IEnumerable<Type> types)
		{
			return types
				.Select(GetGuidFromContentTypeAttribute)
				.Where(s => !string.IsNullOrEmpty(s))
				.ToList();
		}

		private static string GetGuidFromContentTypeAttribute(Type type)
		{
			var attribute = (ContentTypeAttribute)Attribute.GetCustomAttributes(type)
				.SingleOrDefault(a => a is ContentTypeAttribute);
			return attribute?.GUID;
		}
	}
}
