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
	public class WorkspaceSamples
	{
		[TestMethod]
		public void OpenProject()
		{
			var workspace = MSBuildWorkspace.Create();

			var project = workspace.OpenProjectAsync(
                @"..\..\..\WebApplication\WebApplication.csproj").Result;

			var compilation = project.GetCompilationAsync().Result;
			var symbols = compilation.GetSymbolsWithName(s => true);

			foreach (var symbol in symbols.OfType<INamedTypeSymbol>())
			{
				Console.WriteLine(symbol.Kind + " " + symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
			}
		}
	}
}
