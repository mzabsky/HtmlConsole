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
        public List<Selector> Children { get; set; }

        public override bool Match(TagNode path)
        {
            return Children.Any(p => p.Match(path));
        }
    }
}
