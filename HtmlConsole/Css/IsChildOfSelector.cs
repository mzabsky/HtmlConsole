using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsChildOfSelector : CombinatorSelectorBase
    {
        public override bool Match(ElementNode node)
        {
            if (node.Parent == null) return false;

            return SubSelector.Match(node.Parent);
        }

        public override string ToString() => $"[>{SubSelector}]";
    }
}
