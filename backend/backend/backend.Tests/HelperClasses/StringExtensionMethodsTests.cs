using backend.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class StringExtensionMethodsTests
    {
        [TestMethod]
        public void SplitContentString_ReturnsSplitStringAsStringArray()
        {
            var input = "Titel: Azure Fundamentals\r\nCursuscode: AZF\r\nDuur: 5 dagen\r\nStartdatum: 15/06/2020\r\n\r\n";

            var expectedResult = new string[]
            {
                "Titel: Azure Fundamentals",
                "Cursuscode: AZF",
                "Duur: 5 dagen",
                "Startdatum: 15/06/2020",
                "",
                ""
            };

            var actualResult = input.SplitContentString();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
