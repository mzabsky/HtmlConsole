using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlConsole.Extensions
{
    public static class StringExtensions
    {
        public static bool IsIn(this string str, IEnumerable<string> list,
            StringComparison comparison = StringComparison.Ordinal)
        {
            return list.Any(p => string.Compare(str, p, comparison) == 0);
        }

        public static string[] SplitIntoLines(this string str)
        {
            return Regex.Split(str, "\r\n|\r|\n");
        }
    }
}
