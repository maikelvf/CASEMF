using backend.Controllers;
using backend.Data;
using backend.HelperClasses;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace backend.Tests.Controllers
{
    [TestClass]
    public class CursusinstantieControllerTests
    {
        private static CursusinstantieController _controller;
        private static Mock<CursusDBContext> _contextMock;
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
            mockInstantieSet.Setup(m => m.Include("Cursus")).Returns(mockInstantieSet.Object);

            _contextMock = new Mock<CursusDBContext>();
            _contextMock.Setup(m => m.Cursussen).Returns(mockCursusSet.Object);
            _contextMock.Setup(m => m.Cursusinstanties).Returns(mockInstantieSet.Object);

            _controller = new CursusinstantieController(_contextMock.Object);

            _fileHelper = new FileHelper(_contextMock.Object);

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
                "Startdatum: 05/05/2020"
            };

            FileHelper.fileContent = fileContent;
        }

        [TestMethod]
        public void GetCursusinstanties_ReturnsAllCursusinstanties()
        {
            int weeknummer = 27;

            var expectedResult = _contextMock.Object.Cursusinstanties
                .Where(c => c.Startdatum.GetWeekOfYear() == weeknummer)
                .OrderBy(c => c.Startdatum).AsQueryable();

            var actualResult = _controller.GetCursusinstanties(27, 2020);

            CollectionAssert.AreEqual(expectedResult.ToList(), actualResult.ToList());
        }

        [TestMethod]
        public void PostCursusinstanties_ReturnsSuccesResponse()
        {
            // Post method heeft een HttpContext nodig. Opzoeken hoe te mocken, wellicht later.
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

