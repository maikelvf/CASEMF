using backend.Data;
using backend.HelperClasses;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class FileHelperTests
    {
        private static FileHelper _fileHelper;
        private static Mock<CursusDBContext> _contextMock;

        [TestInitialize]
        public void Initialize()
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

            _contextMock = new Mock<CursusDBContext>();
            _contextMock.Setup(m => m.Cursussen).Returns(mockCursusSet.Object);
            _contextMock.Setup(m => m.Cursusinstanties).Returns(mockInstantieSet.Object);

            var fileContent = new string[]
            {
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN",
                "Duur: 5 dagen",
                "Startdatum: 8/10/2018",
                "",
                "Titel: Java Persistence API",
                "Cursuscode: JPA",
                "Duur: 2 dagen",
                "Startdatum: 05/05/2020",
                "",
                "Titel: Blazor",
                "Cursuscode: BLZ",
                "Duur: 5 dagen",
                "Startdatum: 01/01/2020"
            };

            _fileHelper = new FileHelper(_contextMock.Object, fileContent);
        }

        [TestMethod]
        public void GetContentFromFile_ReturnsContentAsStringArray()
        {
            var inputString = "InputString";
            var expectedResult = new string[]
            {
                "InputString"
            };

            using (var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
            {
                var actualResult = FileHelper.GetContentFromFile(fileStream);

                CollectionAssert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public void ReadAllCursussenFromFileContent_ReturnsListWithNewCursussen()
        {
            var expectedResult = new List<Cursus>()
            {
                new Cursus()
                {
                    Code = "BLZ",
                    Duur = 5,
                    Titel = "Blazor"
                }
            };

            var actualResult = _fileHelper.ReadAllCursussenFromFileContent();

            Assert.AreEqual(expectedResult[0].Code, actualResult[0].Code);
            Assert.AreEqual(expectedResult[0].Titel, actualResult[0].Titel);
            Assert.AreEqual(expectedResult[0].Duur, actualResult[0].Duur);
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

            var actualResult = _fileHelper.GetCursusinstantie(5);

            Assert.AreEqual(expectedResult.Cursus.Code, actualResult.Cursus.Code);
            Assert.AreEqual(expectedResult.Startdatum, actualResult.Startdatum);
        }

         [TestMethod]
        public void InitializeCount_SetsCountPropertiesToZero()
        {
            var expectedMessage = "0 nieuwe cursus(sen) toegevoegd, 0 nieuwe instantie(s) toegevoegd.";

            _fileHelper.InitializeCount();

            var resultMessage = _fileHelper.ReturnMessage();

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void IsNewCursus_ReturnsTrueIfCursusNotPresentInListOrDatabase()
        {
            var cursus = new Cursus() { Code = "LNQ", Duur = 2, Titel = "LINQ" };

            var result = _fileHelper.IsNewCursus(new List<Cursus>(), cursus);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNewCursus_ReturnsFalseIfCursusPresentInListOrDatabase()
        {
            var cursus = new Cursus() { Code = "CNETIN", Duur = 5, Titel = "C# Programmeren" };

            var result = _fileHelper.IsNewCursus(new List<Cursus>(), cursus);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsNewInstantie_ReturnsTrueIfInstantieNotPresentInListOrDatabase()
        {
            var cursus = new Cursus()
            {
                Code = "BLZ",
                Duur = 5,
                Titel = "Blazor"
            };

            var instantie = new Cursusinstantie()
            {
                Cursus = cursus,
                Startdatum = new DateTime(2020, 01, 01)
            };

            var result = _fileHelper.IsNewInstantie(new List<Cursusinstantie>(), instantie);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNewInstantie_ReturnsFalseIfInstantiePresentInListOrDatabase()
        {
            var cursus = new Cursus()
            {
                Code = "CNETIN",
                Duur = 5,
                Titel = "C# Programmeren"
            };

            var instantie = new Cursusinstantie()
            {
                Cursus = cursus,
                Startdatum = new DateTime(2020, 01, 01)
            };

            var result = _fileHelper.IsNewInstantie(new List<Cursusinstantie>(), instantie);

            Assert.IsFalse(result);
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => entities.GetEnumerator());
            return mockSet;
        }
    }
}
