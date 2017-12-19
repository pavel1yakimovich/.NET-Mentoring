using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using Roslyn.CustomAnalizer;

namespace Roslyn.CustomAnalizer.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {
        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new RoslynAnalyzerCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new RoslynAnalyzerAnalyzer();
        }

        [TestMethod]
        public void CheckFixProvider()
        {
            var code = @"
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Web;
                using System.Web.Mvc;

                namespace WebApplication.Controllers
                {
                    public class BaseController : Controller
                    {}

                    public class MyController : Controller
                    {
                        // GET: Base
                        public ActionResult Index()
                        {
                            return View();
                        }
                    }
                }";

            var newCode = @"
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Web;
                using System.Web.Mvc;

                namespace WebApplication.Controllers
                {
                    public class BaseController : Controller
                    {}

                    public class MyController : WebApplication.Controllers.BaseController
                    {
                        // GET: Base
                        public ActionResult Index()
                        {
                            return View();
                        }
                    }
                }";


            VerifyCSharpFix(code, newCode);
        }
    }

}