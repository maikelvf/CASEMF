﻿using System;

namespace backend.HelperClasses
{
    public static class ExtractExtensionMethods
    {


        public static string ExtractTitel(this string[] fileContent, int i)
        {
            var line = fileContent[i];
            return line.Substring(line.IndexOf(": ") + 2);
        }

        public static string ExtractCode(this string[] fileContent, int i)
        {
            var line = fileContent[i + 1];
            return line.Substring(line.IndexOf(": ") + 2);
        }

        public static int ExtractDuur(this string[] fileContent, int i)
        {
            var line = fileContent[i + 2];
            return int.Parse(line.Substring(line.IndexOf(": ") + 2, 1));
        }

        public static DateTime ExtractStartdatum(this string[] fileContent, int i)
        {
            var line = fileContent[i + 3];
            return DateTime.Parse(line.Substring(line.IndexOf(": ") + 2));
        }
    }
}