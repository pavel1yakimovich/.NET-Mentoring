using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.CustomAnalizer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RoslynAnalyzerCodeFixProvider)), Shared]
    public class RoslynAnalyzerCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(RoslynAnalyzerAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var document = context.Document;
            var semanticModel = await document.GetSemanticModelAsync(context.CancellationToken);
            var compilation = semanticModel.Compilation;
            var mvcController = compilation.GetTypeByMetadataName("System.Web.Mvc.Controller");

            var root = await document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var classNode = root.FindNode(context.Span) as ClassDeclarationSyntax;
            var classSymbol = semanticModel.GetDeclaredSymbol(classNode);

            var replacedType = classNode.BaseList.Types.Where(t => semanticModel.GetTypeInfo(t.Type).Type == mvcController).Single();
            var newName = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("WebApplication.Controllers.BaseController"))
                .WithLeadingTrivia(replacedType.GetLeadingTrivia())
                .WithTrailingTrivia(replacedType.GetTrailingTrivia());

            context.RegisterCodeFix(
                CodeAction.Create("Change base type",
                    ct => Task.Run(() => document.WithSyntaxRoot(root.ReplaceNode(replacedType, newName)))),
                context.Diagnostics.First());
        }
    }
}