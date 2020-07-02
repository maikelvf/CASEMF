using System;

namespace backend.HelperClasses
{
    public static class StringExtensionMethods
    {
        public static string[] SplitContentString(this string content)
        {
            var splitContent = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return splitContent;
        }
    }
}