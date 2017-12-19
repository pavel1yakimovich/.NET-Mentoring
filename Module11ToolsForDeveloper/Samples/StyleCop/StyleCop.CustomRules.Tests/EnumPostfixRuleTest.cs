﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StyleCop.CustomRules.Tests
{	
	[TestClass]
	public class EnumPostfixRuleTest 
	{
		StyleCopConsole scConsole;
		List<Violation> violationList;

		[TestInitialize]
		public void Initialize()
		{
			scConsole = new StyleCopConsole(null, true, null, null, true, null);
			violationList = new List<Violation>();
			scConsole.ViolationEncountered += scConsole_ViolationEncountered;
			scConsole.OutputGenerated += scConsole_OutputGenerated;
		}

		[TestCleanup]
		public void Destroy()
		{
			scConsole.ViolationEncountered -= scConsole_ViolationEncountered;
			scConsole.OutputGenerated -= scConsole_OutputGenerated;
			scConsole = null;
		}

		void scConsole_OutputGenerated(object sender, OutputEventArgs e)
		{
			Console.WriteLine(e.Output);
		}

		void scConsole_ViolationEncountered(object sender, ViolationEventArgs e)
		{
			violationList.Add(e.Violation);
		}

		[TestMethod]
		public void RightEnumName()
		{
			var project = new CodeProject(1, @".\TestData", new Configuration(new string[] { "DEBUG" }));
			scConsole.Core.Environment.AddSourceCode(project, @".\TestData\Enum_Right.cs", null);

			scConsole.Start(new List<CodeProject>() { project }, true);

			Assert.AreEqual(0, violationList.Count);

		}

		[TestMethod]
		public void WrongEnumName()
		{
			var project = new CodeProject(1, @".\TestData", new Configuration(new string[] { }));
			scConsole.Core.Environment.AddSourceCode(project, @".\TestData\Enum_Wrong.cs", null);

			scConsole.Start(new List<CodeProject>() { project }, true);

			Assert.AreEqual(1, violationList.Count);
			var violation = violationList.First();
			Assert.AreEqual("EP1000", violation.Rule.CheckId);

		}
	}
}
