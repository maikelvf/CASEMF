using Microsoft.VisualStudio.TestTools.UnitTesting;
using backend.Controllers;
using System.Linq;
using Moq;
using backend.Data;
using backend.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace backend.Tests.Controllers
{
    [TestClass]
    public class CursusinstantieControllerTests
    {
        private CursusinstantieController _controller;
        private Mock<CursusDBContext> _contextMock;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetCursusinstanties_ReturnsAllCursusinstanties()
        {
            var cursus = new Cursus { Id = 1, Code = "abc", Duur = 2, Titel = "Cursus 10" };

            var expectedResult = new List<Cursusinstantie>
                {
                    new Cursusinstantie { Id = 1, Startdatum = new DateTime(2020, 01, 01).Date, Cursus = cursus},
                    new Cursusinstantie { Id = 2, Startdatum = new DateTime(2020, 11, 11).Date, Cursus = cursus},
                }.AsQueryable();

            var mockDbSet = GetMockDbSet(expectedResult);
            mockDbSet.Setup(m => m.Include("Cursus")).Returns(mockDbSet.Object);

            _contextMock = new Mock<CursusDBContext>();
            _contextMock.Setup(m => m.Cursusinstanties).Returns(mockDbSet.Object);

            _controller = new CursusinstantieController(_contextMock.Object);
            var actualResult = _controller.GetCursusinstanties();
            CollectionAssert.AreEqual(expectedResult.ToList(), actualResult.ToList());

        }

        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
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

