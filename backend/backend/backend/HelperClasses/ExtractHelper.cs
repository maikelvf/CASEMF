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

        public int ExtractDuur(int i)
        {
            return int.Parse(fileContent[i + 2].Substring(6, 1));
        }

        public string ExtractCode(int i)
        {
            return fileContent[i + 1].Substring(12);
        }

        public string ExtractTitel(int i)
        {
            return fileContent[i].Substring(7);
        }

        public DateTime ExtractStartdatum(int i)
        {
            return DateTime.Parse(fileContent[i + 3].Substring(12));
        }
    }
}