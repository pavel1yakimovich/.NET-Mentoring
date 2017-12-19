using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDT.CustomRules.Tests
{
	public class BaseTest
	{
		protected CodeAnalysisService caService = null;

		protected void InitTest(string ruleId)
		{
			var casFactory = new CodeAnalysisServiceFactory();
			var ruleSettings = new CodeAnalysisRuleSettings()
			{
				new RuleConfiguration(ruleId)
			};

			ruleSettings.DisableRulesNotInSettings = true;

			var caSettings = new CodeAnalysisServiceSettings()
			{
				RuleSettings = ruleSettings
			};

			caService = casFactory.CreateAnalysisService(SqlServerVersion.Sql120, caSettings);
		}

		protected void CleanTest()
		{
			caService = null;
		}
	}
}
