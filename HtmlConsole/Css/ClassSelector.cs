using HtmlConsole.Dom;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    public class ClassSelector : Selector
    {
        public string Class { get; set; }

        public override bool Match(ElementNode node)
        {
            return Class.IsIn(node.Classes);
        }
    }
}
