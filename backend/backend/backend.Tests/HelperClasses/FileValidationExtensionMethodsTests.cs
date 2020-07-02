using backend.HelperClasses;
using backend.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace backend.Tests.HelperClasses
{
    [TestClass]
    public class FileValidationExtensionMethodsTests
    {
        [TestMethod]
        public void IsValidFile_ReturnsValidIfValidFile()
        {
            var inputString = resources.ValidInputString;

            string expectedResult = "Valid";
            string actualResult;

            using (var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
            {
                actualResult = fileStream.IsValidFile();
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        const string invalidTitelInputString = "Titel = C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\n";
        const string invalidCodeInputString = "Titel: C# Programmeren\r\nCode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\n";
        const string invalidDuurInputString = "Titel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\n";
        const string invalidStartdatumInputString = "Titel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8-10-2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\n";
        const string missingEmptyLineInputString = "Titel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 15/10/2018\r\n\r\nTitel: Java Persistence API\r\nCursuscode: JPA\r\nDuur: 2 dagen\r\nStartdatum: 8/10/2018\r\n\r\nTitel: C# Programmeren\r\nCursuscode: CNETIN\r\nDuur: 5 dagen\r\nStartdatum: 8/10/2018\r\n\r\n";

        const string invalidTitelErrorMessage = "Regel 1 is niet in het juiste formaat, regel 1 moet Titel zijn met formaat: 'Titel: <titel>'";
        const string invalidCodeErrorMessage = "Regel 2 is niet in het juiste formaat, regel 2 moet Cursuscode zijn met formaat: 'Cursuscode: <code>'";
        const string invalidDuurErrorMessage = "Regel 3 is niet in het juiste formaat, regel 3 moet Cursusduur zijn met formaat: 'Duur: # dagen'";
        const string invalidStartdatumErrorMessage = "Regel 4 is niet in het juiste formaat, regel 4 moet Startdatum zijn met formaat: 'Startdatum: 01/01/2020'";
        const string shouldBeEmptyLineErrorMessage = "Regel 5 is niet in het juiste formaat: Regel 5 moet een witregel zijn";

        [DataTestMethod]
        [DataRow(invalidTitelInputString, invalidTitelErrorMessage)]
        [DataRow(invalidCodeInputString, invalidCodeErrorMessage)]
        [DataRow(invalidDuurInputString, invalidDuurErrorMessage)]
        [DataRow(invalidStartdatumInputString, invalidStartdatumErrorMessage)]
        [DataRow(missingEmptyLineInputString, shouldBeEmptyLineErrorMessage)]
        public void IsValidFile_ReturnsCorrectErrorMessageIfInvalidFile(string inputString, string expectedErrorMessage)
        {
            string actualResult;

            using (var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
            {
                actualResult = fileStream.IsValidFile();
            }

            Assert.AreEqual(expectedErrorMessage, actualResult);
        }
        
        [TestMethod]
        public void IsValidTitel_ReturnsTrueIfValidTitel()
        {
            var validTitel = "Titel: #Geldige Titel!";

            var result = validTitel.IsValidTitel();

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("Dit is geen geldige titel...")]
        [DataRow("Titel:Dit ook niet")]
        [DataRow("Titel nog steeds niet")]
        [DataRow("Titel = Laat maar dit wordt niets...")]
        public void IsValidTitel_ReturnsFalseIfInvalidTitel(string titel)
        {
            var invalidTitel = titel;

            var result = invalidTitel.IsValidTitel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidCode_ReturnsTrueIfValidCode()
        {
            var validCode = "Cursuscode: ABC";

            var result = validCode.IsValidCode();

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("Code: GeenGeldigeCode")]
        [DataRow("Cursuscode : Nope")]
        [DataRow("Cursuscode:GeenGeldigeCode")]
        [DataRow("Cursuscode= GeenGeldigeCode")]
        public void IsValidCode_ReturnsFalseIfInvalidCode(string code)
        {
            var invalidCode = code;

            var result = invalidCode.IsValidCode();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidDuur_ReturnsTrueIfValidDuur()
        {
            var validDuur = "Duur: 3 dagen";

            var result = validDuur.IsValidDuur();

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("Duur: 2")]
        [DataRow("Hier valt geen cursusduur in te ontdekken...")]
        [DataRow("Duur: 1 dag")]
        [DataRow("Duur = 2 dagen")]
        public void IsValidDuur_ReturnsFalseIfInvalidDuur(string duur)
        {
            var invalidDuur = duur;

            var result = invalidDuur.IsValidDuur();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidStartdatum_ReturnsTrueIfValidStartdatum()
        {
            var validStartdatum = "Startdatum: 01/01/2020";

            var result = validStartdatum.IsValidStartdatum();

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("01/01/2020")]
        [DataRow("Startdatum: 01-01-2020")]
        [DataRow("Startdatum: 2020/01/01")]
        [DataRow(@"Startdatum: 01\01\2020")]
        [DataRow("Startdatum: 01/01-2020")]
        public void IsValidStartdatum_ReturnsFalseIfInvalidStartdatum(string datum)
        {
            var invalidStartdatum = datum;

            var result = invalidStartdatum.IsValidStartdatum();

            Assert.IsFalse(result);
        }
    }
}
