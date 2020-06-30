﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using backend.Controllers;
using System.Linq;
using Moq;
using backend.Data;
using backend.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace backend.Tests.Controllers
{
    [TestClass]
    public class CursusControllerTests
    {
        private CursusController _controller;
        private Mock<CursusDBContext> _contextMock;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetCursussen_ReturnsAllCursussen()
        {
            var expectedResult = new List<Cursus>
                {
                    new Cursus { Id = 1, Code = "abc", Duur = 2, Titel = "Cursus 10" },
                    new Cursus { Id = 2, Code = "def", Duur = 5, Titel = "Cursus 20" },
                    new Cursus { Id = 3, Code = "ghi", Duur = 1, Titel = "Cursus 30" },
            }.AsQueryable();

            var mockDbSet = GetMockDbSet(expectedResult);
            mockDbSet.Setup(m => m.Include("Cursusinstanties")).Returns(mockDbSet.Object);

            _contextMock = new Mock<CursusDBContext>();
            _contextMock.Setup(m => m.Cursussen).Returns(mockDbSet.Object);

            _controller = new CursusController(_contextMock.Object);
            var actualResult = _controller.GetCursussen();
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
