using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandaloneSamples
{
	[TestClass]
	public class CompilationSamples
	{
		[TestMethod]
		public void SimpleCompile()
		{
			var compiler = CSharpCompilation.Create("test_assembly")
				.AddSyntaxTrees(new SyntaxTree[]
				{
					CSharpSyntaxTree.ParseText(Properties.Resources.Class1),
					CSharpSyntaxTree.ParseText(Properties.Resources.Class2)
				})
				.AddReferences(new MetadataReference[]
				{
					MetadataReference.CreateFromFile(Assembly.Load("mscorlib").Location),
					MetadataReference.CreateFromFile(Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").Location)
				})
				.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

			var result =  compiler.Emit("test_assembly.dll");

			Assert.IsTrue(result.Success);
		}
	}
}
