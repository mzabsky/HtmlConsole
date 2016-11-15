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

        public override SelectorMatch Match(ElementNode node)
        {
            // Find the greatest specificity of a successful child match
            var specificity = Children
                .Select(p => p.Match(node))
                .Where(p => p.IsSuccess)
                .Select(p => p.Specificity)
                .OrderByDescending(p => p)
                .FirstOrDefault();

            if (specificity == null)
            {
                return new SelectorMatch(false, new Specificity());
            }

            return new SelectorMatch(true, specificity);
        }

        public override string ToString() => $"[OR {string.Join(" OR ", Children.Select(p => p.ToString()))}]";
    }
}
