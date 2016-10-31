using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsDescendantOfSelector : Selector
    {
        public Selector SubSelector { get; set; }

        public override bool Match(ElementNode node)
        {
            // TODO: recusion very unnecessary here
            if (SubSelector.Match(node)) return true;

            if (node.Parent == null) return false;

            return Match(node.Parent);
        }
    }
}
