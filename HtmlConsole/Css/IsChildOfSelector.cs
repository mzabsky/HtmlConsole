using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsChildOfSelector : Selector
    {
        public Selector SubSelector { get; set; }

        public override bool Match(ElementNode node)
        {
            if (node.Parent == null) return false;

            return SubSelector.Match(node.Parent);
        }
    }
}
