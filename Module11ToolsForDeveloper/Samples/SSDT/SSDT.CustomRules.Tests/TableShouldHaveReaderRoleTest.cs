using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;

namespace SSDT.CustomRules.Tests
{
	[TestClass]
	public class TableShouldHaveReaderRoleTest : BaseTest
	{
		string ruleId = "SSDT.CustomRules.Permission.EP1000";
		
		[TestInitialize]
		public void Init()
		{
			InitTest(ruleId);
		}

		[TestCleanup]
		public void Clean()
		{
			CleanTest();
		}


		[TestMethod]
		public void TableWithGrantSelectPermission()
		{
			var model = new TSqlModel(SqlServerVersion.Sql120, new TSqlModelOptions());
			model.AddObjects(TableShouldHaveReaderRoleTestData.TestData.TableWithGrantSelectPermission);

			var result = caService.Analyze(model);

			Assert.AreEqual(0, result.Problems.Count);
		}

		[TestMethod]
		public void TableWithNoGrantSelectPermission()
		{
			var model = new TSqlModel(SqlServerVersion.Sql120, new TSqlModelOptions());
			model.AddObjects(TableShouldHaveReaderRoleTestData.TestData.TableWithNoGrantSelectPermission);

			var result = caService.Analyze(model);
			
			Assert.AreEqual(1, result.Problems.Count);

			var problem = result.Problems[0];
			Assert.AreEqual(ruleId, problem.RuleId);
			Assert.AreEqual(SqlRuleProblemSeverity.Warning, problem.Severity);
			Assert.IsTrue(model.CollationComparer.Equals(new ObjectIdentifier("dbo","T1"), problem.ModelElement.Name));
		}
	}
}
