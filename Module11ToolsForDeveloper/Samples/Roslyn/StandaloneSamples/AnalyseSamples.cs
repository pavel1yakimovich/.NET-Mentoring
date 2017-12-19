using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandaloneSamples
{
	[TestClass]
	public class AnalyseSamples
	{
		[TestMethod]
		public void SearchControllers()
		{
			var workspace = MSBuildWorkspace.Create();

			var project = workspace.OpenProjectAsync(
                @"..\..\..\..\AnalyzableProjects\WebApplication\WebApplication.csproj").Result;

			var compilation = project.GetCompilationAsync().Result;
			var symbols = compilation.GetSymbolsWithName(s => true, SymbolFilter.Type).OfType<INamedTypeSymbol>();

			var mvcController = compilation.GetTypeByMetadataName("System.Web.Mvc.Controller");

			foreach (var symbol in symbols)
			{
				var baseTypes = GetBaseClasses(symbol, compilation.ObjectType);
				if (baseTypes.Contains(mvcController))
				{
					Console.WriteLine(symbol.ToDisplayString());
					var span = symbol.Locations[0].GetMappedLineSpan();
					Console.WriteLine(span.Path);
					Console.WriteLine(span.StartLinePosition);
					Console.WriteLine(span.Span);
				}
			}
		}

		public ImmutableArray<INamedTypeSymbol> GetBaseClasses(INamedTypeSymbol type, INamedTypeSymbol objectType)
		{
			if (type == null || type.TypeKind == TypeKind.Error)
				return ImmutableArray<INamedTypeSymbol>.Empty;

			if (type.BaseType != null && type.BaseType.TypeKind != TypeKind.Error)
				return GetBaseClasses(type.BaseType, objectType).Add(type.BaseType);

			return ImmutableArray<INamedTypeSymbol>.Empty;
		}
	}
}
