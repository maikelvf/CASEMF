using System.IO;
using System.Text.RegularExpressions;

namespace backend.HelperClasses
{
    public static class FileValidationExtensionMethods
    {
        private static string[] _fileContent;

        public static string FileIsValid(this Stream file)
        {
            _fileContent = FileHelper.GetContentFromFile(file);

            for (int lineNumber = 0; lineNumber < _fileContent.Length - 1; lineNumber++)
            {
                var line = _fileContent[lineNumber];

                switch (lineNumber % 5)
                {
                    case 0:
                        if (!line.IsValidTitel())
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Titel zijn met formaat: 'Titel: <titel>'";
                        }
                        break;

                    case 1:
                        if (!line.IsValidCode())
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Cursuscode zijn met formaat: 'Cursuscode: <code>'";
                        }
                        break;

                    case 2:
                        if (!line.IsValidDuur())
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Cursusduur zijn met formaat: 'Duur: # dagen'";
                        }
                        break;

                    case 3:
                        if (!line.IsValidStartdatum())
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Startdatum zijn met formaat: 'Startdatum: 01/01/2020'";
                        }
                        break;

                    case 4:
                        if (!string.IsNullOrEmpty(line))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat: Regel {lineNumber + 1} moet een witregel zijn";
                        }
                        break;

                    default:
                        return "Valid";
                }
            }

            return "Valid";
        }

        public static bool IsValidTitel(this string line)
        {
            // Regex voor "Titel: <titel>"
            var reg = new Regex(@"^Titel:\s.+");
            if (reg.IsMatch(line))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidCode(this string line)
        {
            // Regex voor "Cursuscode: <code>"
            var reg = new Regex(@"^Cursuscode:\s.+");
            if (reg.IsMatch(line))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidDuur(this string line)
        {
            // Regex voor "Duur: # dagen"
            var reg = new Regex(@"^Duur:\s\d\sdagen");
            if (reg.IsMatch(line))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidStartdatum(this string line)
        {
            // Regex voor "Startdatum: ##/##/####"
            var reg = new Regex(@"^Startdatum:\s\d{1,2}\/\d{1,2}\/\d{4}$");
            if (reg.IsMatch(line))
            {
                return true;
            }
            return false;
        }
    }
}