using backend.HelperClasses;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace backend.Tests.HelperClasses.FileHelperTests
{
    [TestClass]
    public class ReadFromFileTests
    {
        private static TestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _testContext)
        {
            FileHelper.fileContent = new string[]
            {
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN",
                "Duur: 5 dagen",
                "Startdatum: 8/10/2018",
                "Titel: Java Persistence API",
                "Cursuscode: JPA",
                "Duur: 2 dagen",
                "Startdatum: 15/10/2018"
            };
        }

        [TestMethod]
        public void GetCursus_ReturnsNewCursusWithParametersFromFile()
        {
            var expectedResult = new Cursus()
            {
                Titel = "C# Programmeren",
                Code = "CNETIN",
                Duur = 5
            };

            var actualResult = FileHelper.GetCursus(0);

            Assert.AreEqual(expectedResult.Titel, actualResult.Titel);
            Assert.AreEqual(expectedResult.Code, actualResult.Code);
            Assert.AreEqual(expectedResult.Duur, actualResult.Duur);
        }

        [TestMethod]
        public void GetCursusinstantie_ReturnsNewCursusinstantieWithParametersFromFile()
        {
            var cursus = new Cursus()
            {
                Titel = "Java Persistence API",
                Code = "JPA",
                Duur = 2
            };

            var expectedResult = new Cursusinstantie()
            {
                Cursus = cursus,
                Startdatum = new DateTime(2018, 10, 15)
            };

            var actualResult = FileHelper.GetCursusinstantie(4);

            Assert.AreEqual(expectedResult.Cursus, actualResult.Cursus);
            Assert.AreEqual(expectedResult.Startdatum, actualResult.Startdatum);
        }
    }
}
