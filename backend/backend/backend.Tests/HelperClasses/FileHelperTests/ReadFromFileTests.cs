using backend.Data;
using backend.HelperClasses;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace backend.Tests.HelperClasses.FileHelperTests
{
    [TestClass]
    public class ReadFromFileTests
    {
        private static FileHelper _fileHelper;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var cursus1 = new Cursus() { Code = "CNETIN", Duur = 5, Titel = "C# Programmeren" };
            var cursus2 = new Cursus() { Code = "JPA", Duur = 2, Titel = "Java Persistence API" };

            var cursussen = new List<Cursus>()
            {
                cursus1,
                cursus2
            }.AsQueryable();
            
            var mockCursusSet = GetMockDbSet(cursussen);

            var instanties = new List<Cursusinstantie>()
            {
                new Cursusinstantie() { Cursus = cursus1, Startdatum = new DateTime(2020, 01, 01) },
                new Cursusinstantie() { Cursus = cursus1, Startdatum = new DateTime(2020, 11, 11) },
                new Cursusinstantie() { Cursus = cursus2, Startdatum = new DateTime(2020, 05, 05) }
            }.AsQueryable();

            var mockInstantieSet = GetMockDbSet(instanties);

            var contextMock = new Mock<CursusDBContext>();
            contextMock.Setup(m => m.Cursussen).Returns(mockCursusSet.Object);
            contextMock.Setup(m => m.Cursusinstanties).Returns(mockInstantieSet.Object);
            _fileHelper = new FileHelper(contextMock.Object);

            _fileHelper.fileContent = new string[]
            {
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN",
                "Duur: 5 dagen",
                "Startdatum: 8/10/2018",
                "Titel: Java Persistence API",
                "Cursuscode: JPA",
                "Duur: 2 dagen",
                "Startdatum: 05/05/2020"
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

            var actualResult = _fileHelper.GetCursus(0);

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
                Startdatum = new DateTime(2020, 05, 05)
            };

            var actualResult = _fileHelper.GetCursusinstantie(4);

            Assert.AreEqual(expectedResult.Cursus.Code, actualResult.Cursus.Code);
            Assert.AreEqual(expectedResult.Startdatum, actualResult.Startdatum);
        }

        [TestMethod]
        public void ReturnMessage_ReturnsCompleteMessageWhenDuplicatesPresent()
        {
            _fileHelper.ReadAllCursussenFromFileContent();
            _fileHelper.ReadAllInstantiesFromFileContent();

            var expectedMessage = "0 nieuwe cursus(sen) toegevoegd, 1 nieuwe instantie(s) toegevoegd." +
                                  " 2 cursus(sen) dubbel, niet toegevoegd." +
                                  " 1 cursusinstantie(s) dubbel, niet toegevoegd.";

            var actualMessage = _fileHelper.ReturnMessage();

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InitializeCount_SetsCountPropertiesToZero()
        {
            var expectedMessage = "0 nieuwe cursus(sen) toegevoegd, 0 nieuwe instantie(s) toegevoegd.";

            _fileHelper.InitializeCount();

            var resultMessage = _fileHelper.ReturnMessage();

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }
    }
}
