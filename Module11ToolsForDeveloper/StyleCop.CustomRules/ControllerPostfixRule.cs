using StyleCop.CSharp;
using System.Web.Mvc;

namespace StyleCop.CustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerPostfixRule : SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument csDocument = (CsDocument)document;
            csDocument.WalkDocument(new CodeWalkerElementVisitor<object>(this.VisitElement));
        }

        private bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            var controller = element as Class;
            if (controller != null && controller.BaseClass == typeof(Controller).Name)
            {
                if (!controller.Name.EndsWith("Controller", System.StringComparison.Ordinal))
                {
                    this.AddViolation(element, "ControllerPostfixRule");
                }

                return false;
            }
            return true;
        }
    }
}