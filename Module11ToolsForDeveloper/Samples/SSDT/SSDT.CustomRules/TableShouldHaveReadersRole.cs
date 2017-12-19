using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDT.CustomRules
{
	[ExportCodeAnalysisRule("SSDT.CustomRules.Permission.EP1000", "Permission for ReaderRole")]
	public class TableShouldHaveReaderRole : SqlCodeAnalysisRule 
	{
		public TableShouldHaveReaderRole()
		{
			this.SupportedElementTypes = new[] { ModelSchema.Table };
		}

		public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
		{
			var table = ruleExecutionContext.ModelElement;
			var permissions = table.GetReferencing(Permission.SecuredObject);
			var readerRole = new ObjectIdentifier("ReaderRole");

			var comparer = ruleExecutionContext.SchemaModel.CollationComparer;

			foreach (var permission in permissions)
			{
				var permissionType = (PermissionType)permission.GetProperty(Permission.PermissionType);
				var permissionAction = (PermissionAction)permission.GetProperty(Permission.PermissionAction);

				var grantees = permission.GetReferenced(Permission.Grantee).Select(grantee => grantee.Name);

				if (grantees.Contains(readerRole, comparer) && permissionAction == PermissionAction.Grant && permissionType == PermissionType.Select)
					return new List<SqlRuleProblem>();
			}

			return new List<SqlRuleProblem>() { new SqlRuleProblem("Table don't have permission for [ReaderRole]", table) };
		}
	}
}
