using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsDescendantOfSelector : Selector
    {
        public Selector SubSelector { get; set; }

        public override bool Match(ElementNode node)
        {
            // TODO: recusion very unnecessary here
            if (node.Parent == null) return false;

            if (SubSelector.Match(node.Parent)) return true;

            return Match(node.Parent);
        }
    }
}
