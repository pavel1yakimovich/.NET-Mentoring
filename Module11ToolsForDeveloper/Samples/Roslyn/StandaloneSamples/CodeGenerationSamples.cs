using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandaloneSamples
{
	[TestClass]
	public class CodeGenerationSamples
	{
		[TestMethod]
		public void LowLevelApiTest()
		{
			var unit = SyntaxFactory.CompilationUnit()
				.AddMembers(
					SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("EPAM.Test"))
						.AddMembers(
							SyntaxFactory.ClassDeclaration("MyClass")
								.AddMembers(
									SyntaxFactory.PropertyDeclaration(
										SyntaxFactory.ParseTypeName("System.String"),
										"A")
										.AddAccessorListAccessors(
											SyntaxFactory.AccessorDeclaration(
												SyntaxKind.GetAccessorDeclaration)
												.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
											SyntaxFactory.AccessorDeclaration(
												SyntaxKind.SetAccessorDeclaration)
												.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
								)
						)
				).NormalizeWhitespace();

			Console.WriteLine(unit.ToFullString());
		}

		[TestMethod]
		public void UseSyntaxGenerator()
		{
			var workspace = new AdhocWorkspace();
			var generator = SyntaxGenerator.GetGenerator(workspace, 
				LanguageNames.CSharp);

			var unit = generator.CompilationUnit(
				generator.NamespaceDeclaration(
					"EPAM",
					generator.ClassDeclaration(
						"MyClass",
						members: new SyntaxNode[]
							{
								generator.FieldDeclaration(
									"_a",
									generator.TypeExpression(SpecialType.System_String)),

								generator.PropertyDeclaration(
									"A", 
									generator.TypeExpression(SpecialType.System_String),
									accessibility: Accessibility.Public, 
									getAccessorStatements: 
										new SyntaxNode[] {
												generator.ReturnStatement(
													generator.MemberAccessExpression(
														generator.ThisExpression(),
														"_a"))
											},
									setAccessorStatements:
										new SyntaxNode[] {
												generator.AssignmentStatement(
													generator.MemberAccessExpression(
														generator.ThisExpression(),
														"_a"),
													generator.IdentifierName("value"))
										})
							}))
					).NormalizeWhitespace();

			
			Console.WriteLine(unit.ToFullString());
		}
	}
}
