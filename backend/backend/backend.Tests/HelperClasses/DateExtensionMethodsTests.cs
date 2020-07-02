using backend.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class DateExtensionMethodsTests
    {
        [DataTestMethod]
        [DataRow("01/01/2020", 1)]
        [DataRow("05/05/2020", 19)]
        [DataRow("01/07/2020", 27)]
        [DataRow("25/12/2020", 52)]
        public void MyTestMethod(string input, int weeknummer)
        {
            var date = DateTime.Parse(input);
            var week = date.GetWeekOfYear();

            Assert.AreEqual(weeknummer, week);
        }
    }
}
