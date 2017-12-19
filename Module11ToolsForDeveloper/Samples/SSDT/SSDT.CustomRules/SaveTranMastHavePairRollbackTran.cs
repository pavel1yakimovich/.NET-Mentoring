using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDT.CustomRules
{
	[ExportCodeAnalysisRule("SSDT.CustomRules.Transactions.EP1001", "Save tran mast have pair Rollback tran")]
	public class SaveTranMastHavePairRollbackTran : SqlCodeAnalysisRule
	{
		public SaveTranMastHavePairRollbackTran()
		{
			this.SupportedElementTypes = new[] { ModelSchema.Procedure };
		}

		public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
		{
			var procedure = ruleExecutionContext.ModelElement;
			var script = procedure.GetScript();

			var parser = new TSql120Parser(true);
			IList<ParseError> errors = new List<ParseError>();
			var fragment = parser.Parse(new StringReader(script), out errors);

			var visitor = new TransactionVisitor();

			fragment.Accept(visitor);
			var dif = visitor.GetDifference();

			if (dif.Any())
				return
					new List<SqlRuleProblem>() {
					new SqlRuleProblem("No balanced transaction", procedure)};

			return new List<SqlRuleProblem>();
		}
	}

	public class TransactionVisitor : TSqlFragmentVisitor
	{
		HashSet<string> saveTransaction = new HashSet<string>();
		HashSet<string> rollbackTransaction = new HashSet<string>();

		public override void ExplicitVisit(SaveTransactionStatement node)
		{
			if (node.Name != null)
				saveTransaction.Add(node.Name.Value);
		}

		public override void ExplicitVisit(RollbackTransactionStatement node)
		{
			if (node.Name != null)
				rollbackTransaction.Add(node.Name.Value);
		}

		public IEnumerable<string> GetDifference()
		{
			return saveTransaction.Except(rollbackTransaction).Union(rollbackTransaction.Except(saveTransaction));
		}
	}
}
