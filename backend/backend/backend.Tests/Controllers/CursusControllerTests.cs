using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using backend.Controllers;
using System.Linq;
using Moq;
using backend.Data;
using backend.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace backend.Tests.Controllers
{
    [TestClass]
    public class CursusControllerTests
    {
        private CursusDBContext _context;
        private CursusController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<CursusDBContext>().Object;

            _context.Cursusinstanties = GetDbSet(new List<Cursusinstantie>()
            {
                new Cursusinstantie() { Startdatum = "01/01/2020", Cursus = new Cursus() { Code = "aaa", Duur = 1, Titel = "Cursus 1" }},
                new Cursusinstantie() { Startdatum = "05/05/2020", Cursus = new Cursus() { Code = "bbb", Duur = 3, Titel = "Cursus 2" }},
                new Cursusinstantie() { Startdatum = "11/11/2020", Cursus = new Cursus() { Code = "ccc", Duur = 5, Titel = "Cursus 3" }}
            });

            _context.Cursussen = GetDbSet(new List<Cursus>()
            {
                new Cursus() { Code = "ddd", Duur = 2, Titel = "Cursus 4" },
                new Cursus() { Code = "eee", Duur = 4, Titel = "Cursus 5" },
                new Cursus() { Code = "fff", Duur = 1, Titel = "Cursus 6" }
            });

            _controller = new CursusController(_context);
        }

        [TestMethod]
        public void GetCursussen_ReturnsAllCursussen()
        {
            var result = _controller.GetCursussen();

            Assert.IsInstanceOfType(result, typeof(DbSet<Cursus>));
            Assert.AreEqual(result.Count(), 3);
        }

        private static DbSet<T> GetDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}

