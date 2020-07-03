using backend.Data;
using backend.HelperClasses;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class ReturnMessageTests
    {
        private static FileHelper _fileHelper;
        private static Mock<CursusDBContext> _contextMock;

        [TestInitialize]
        public void Initialize()
        {
            var cursus1 = new Cursus() { Code = "CNETIN", Duur = 5, Titel = "C# Programmeren" };
            var cursus2 = new Cursus() { Code = "JPA", Duur = 2, Titel = "Java Persistence API" };
            var cursus3 = new Cursus() { Code = "BLZ", Duur = 5, Titel = "Blazor" };

            var cursussen = new List<Cursus>()
            {
                cursus1,
                cursus2,
                cursus3
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
        public void ReturnMessage_ReturnsCompleteMessageWhenDuplicatesPresent()
        {
            _fileHelper.ReadAllCursussenFromFileContent();
            _fileHelper.ReadAllInstantiesFromFileContent();

            var expectedMessage = "0 nieuwe cursus(sen) toegevoegd, 2 nieuwe instantie(s) toegevoegd." +
                                  " 3 cursus(sen) dubbel, niet toegevoegd." +
                                  " 1 cursusinstantie(s) dubbel, niet toegevoegd.";

            var actualMessage = _fileHelper.ReturnMessage();

            Assert.AreEqual(expectedMessage, actualMessage);
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
