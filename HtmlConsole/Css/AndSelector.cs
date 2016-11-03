using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class AndSelector : Selector
    {
        public List<Selector> Children { get; set; } = new List<Selector>();

        public override bool Match(ElementNode path)
        {
            return Children.All(p => p.Match(path));
        }

        public override string ToString() => $"[AND {string.Join(" AND ", Children.Select(p => p.ToString()))}]";
    }
}