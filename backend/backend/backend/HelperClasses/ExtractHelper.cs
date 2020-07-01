using System;

namespace backend.HelperClasses
{
    public class ExtractHelper
    {
        public string[] fileContent;

        public ExtractHelper(string[] content)
        {
            fileContent = content;
        }

        public string ExtractTitel(int i)
        {
            var line = fileContent[i];
            return line.Substring(line.IndexOf(": ") + 2);
        }

        public string ExtractCode(int i)
        {
            var line = fileContent[i + 1];
            return line.Substring(line.IndexOf(": ") + 2);
        }

        public int ExtractDuur(int i)
        {
            var line = fileContent[i + 2];
            return int.Parse(line.Substring(line.IndexOf(": ") + 2, 1));
        }

        public DateTime ExtractStartdatum(int i)
        {
            var line = fileContent[i + 3];
            return DateTime.Parse(line.Substring(line.IndexOf(": ") + 2));
        }
    }
}