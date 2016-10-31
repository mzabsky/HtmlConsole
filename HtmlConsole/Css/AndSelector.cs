using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class AndSelector : Selector
    {
        public List<Selector> Children { get; set; }

        public override bool Match(TagNode path)
        {
            return Children.All(p => p.Match(path));
        }
    }
}