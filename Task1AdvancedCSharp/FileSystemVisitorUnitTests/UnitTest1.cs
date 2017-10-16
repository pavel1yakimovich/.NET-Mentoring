﻿using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1AdvancedCSharp;
using System.Linq;
using System.Collections.Generic;

namespace FileSystemVisitorUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IDirectoryAdapter> mockDirectory;

        [TestInitialize]
        public void TestInit()
        {
            mockDirectory = new Mock<IDirectoryAdapter>();
            mockDirectory.Setup(dir => dir.GetDirectories("mock")).Returns(new string[] { "folder1", "folder2", "folder3" });
            mockDirectory.Setup(dir => dir.GetDirectories("folder1")).Returns(new string[] { "folder11", "folder12" });
            mockDirectory.Setup(dir => dir.GetDirectories("folder2")).Returns(new string[] { "folder21", "folder22" });
            mockDirectory.SetupSequence(dir => dir.GetFiles("folder11")).Returns(new string[] { "file112", "file111" });
            mockDirectory.SetupSequence(dir => dir.GetFiles("folder1")).Returns(new string[] { "file12", "file11" });
            mockDirectory.SetupSequence(dir => dir.GetFiles("folder22")).Returns(new string[] { "file222", "file221" });
            mockDirectory.SetupSequence(dir => dir.GetFiles("mock")).Returns(new string[] { "file2", "file1" });
        }

        [TestMethod]
        public void RegularMode()
        {
            var fsv = new FileSystemVisitor("mock", mockDirectory.Object);

            var expected = new string[] {"folder1", "folder11", "file112", "file111", "folder12", "file12", "file11",
                "folder2", "folder21", "folder22", "file222", "file221", "folder3", "file2", "file1"};

            var actualArray = new List<string>();
            foreach (var item in fsv)
            {
                actualArray.Add(item);
            }

            Assert.IsTrue(expected.SequenceEqual(actualArray));
        }
    }
}
