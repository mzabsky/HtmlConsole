using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole
{
    public static class StringExtensions
    {
        public static bool IsIn(this string str, IEnumerable<string> list,
            StringComparison comparison = StringComparison.Ordinal)
        {
            return list.Any(p => string.Compare(str, p, comparison) == 0);
        }
    }
}
