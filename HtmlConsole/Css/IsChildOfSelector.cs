using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsChildOfSelector : CombinatorSelectorBase
    {
        public IsChildOfSelector(Selector subSelector) : base(subSelector)
        {
        }

        public override SelectorMatch Match(ElementNode node)
        {
            if (node.Parent == null) return new SelectorMatch(false, new Specificity());

            return SubSelector.Match(node.Parent);
        }

        public override string ToString() => $"[>{SubSelector}]";
    }
}
