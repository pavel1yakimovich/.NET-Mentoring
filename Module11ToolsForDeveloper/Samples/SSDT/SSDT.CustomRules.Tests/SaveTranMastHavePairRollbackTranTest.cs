using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SqlServer.Dac.Model;

namespace SSDT.CustomRules.Tests
{
	[TestClass]
	public class SaveTranMastHavePairRollbackTranTest : BaseTest
	{
		string ruleId = "SSDT.CustomRules.Transactions.EP1001";

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
		public void BalancedTranNames()
		{
			var model = new TSqlModel(SqlServerVersion.Sql120, new TSqlModelOptions());
			model.AddObjects(SaveTranMastHavePairRollbackTranTestData.TestData.SaveTransactionSampleProcedureBalanced);

			var result = caService.Analyze(model);

			Assert.AreEqual(0, result.Problems.Count);

		}

		[TestMethod]
		public void NotBalancedTranNames()
		{
			var model = new TSqlModel(SqlServerVersion.Sql120, new TSqlModelOptions());
			model.AddObjects(SaveTranMastHavePairRollbackTranTestData.TestData.SaveTransactionSampleProcedureNotBalanced);

			var result = caService.Analyze(model);

			Assert.AreEqual(1, result.Problems.Count);

		}

	}
}
