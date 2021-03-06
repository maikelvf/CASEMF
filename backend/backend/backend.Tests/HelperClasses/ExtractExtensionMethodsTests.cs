﻿using backend.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class ExtractExtensionMethodsTests
    {

        private static string[] _fileContent;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _fileContent = new string[]
            {
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN",
                "Duur: 5 dagen",
                "Startdatum: 8/10/2018",
                "",
                "Titel: Java Persistence API",
                "Cursuscode: JPA",
                "Duur: 2 dagen",
                "Startdatum: 15/10/2018"
            };
        }

        [TestMethod]
        public void ExtractDuur_ShouldReturnDuurFromLines()
        {
            var expectedResult = 2;

            var actualResult = _fileContent.ExtractDuur(5);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ExtractCode_ShouldReturnCodeFromLines()
        {
            var expectedResult = "CNETIN";

            var actualResult = _fileContent.ExtractCode(0);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ExtractTitel_ShouldReturnTitelFromLines()
        {
            var expectedResult = "Java Persistence API";

            var actualResult = _fileContent.ExtractTitel(5);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ExtractStartdatum_ShouldReturnStartdatumFromLines()
        {
            var expectedResult = new DateTime(2018, 10, 8);

            var actualResult = _fileContent.ExtractStartdatum(0);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
