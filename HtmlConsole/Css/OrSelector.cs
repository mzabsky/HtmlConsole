using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    /// <summary>
    /// Represents ,
    /// </summary>
    public class OrSelector : Selector
    {
        public List<Selector> Children { get; set; } = new List<Selector>();

        public override bool Match(ElementNode path)
        {
            return Children.Any(p => p.Match(path));
        }

        public override string ToString() => $"[OR {string.Join(" OR ", Children.Select(p => p.ToString()))}]";
    }
}
