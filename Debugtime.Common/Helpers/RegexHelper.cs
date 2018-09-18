using System;
using System.Text.RegularExpressions;

namespace Debugtime.Common.Helpers
{
    public static class RegexHelper
    {
        public static string CleanHttpResponceString(string input)
        {
            return Regex.Replace(input, "\"?", String.Empty);
        }

        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        public static string[] Split(string input, string pattern)
        {
            return Regex.Split(input, pattern);
        }
    }
}