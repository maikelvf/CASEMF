using backend.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace backend.HelperClasses
{
    public static class FileValidationExtensionMethods
    {
        private static string[] _fileContent;

        public static string IsValidFile(this Stream file)
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
                            return string.Format(resources.InvalidTitel, lineNumber + 1);
                        }
                        break;

                    case 1:
                        if (!line.IsValidCode())
                        {
                            return string.Format(resources.InvalidCode, lineNumber + 1);
                        }
                        break;

                    case 2:
                        if (!line.IsValidDuur())
                        {
                            return string.Format(resources.InvalidDuur, lineNumber + 1);
                        }
                        break;

                    case 3:
                        if (!line.IsValidStartdatum())
                        {
                            return string.Format(resources.InvalidStartdatum, lineNumber + 1);
                        }
                        break;

                    case 4:
                        if (!string.IsNullOrEmpty(line))
                        {
                            return string.Format(resources.ShouldBeEmptyLine, lineNumber + 1);
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