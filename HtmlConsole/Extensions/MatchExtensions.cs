using Eto.Parse;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Extensions
{
    public static class MatchExtensions
    {
        public static IEnumerable<Match> ExceptWhitespace(this IEnumerable<Match> matches)
        {
            return matches.Where(p => p.Name != "S");
        }
    }
}
