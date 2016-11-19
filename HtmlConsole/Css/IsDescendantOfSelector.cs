using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IsDescendantOfSelector : CombinatorSelectorBase
    {
        public IsDescendantOfSelector(Selector subSelector) : base(subSelector)
        {
        }

        public override SelectorMatch Match(ElementNode node)
        {
            // TODO: recusion very unnecessary here
            if (node.Parent == null) return new SelectorMatch(false, new Specificity());

            var subselectorMatch = SubSelector.Match(node.Parent);
            if (subselectorMatch.IsSuccess) return subselectorMatch;

            // Try to go up the DOM in order to find a match
            return Match(node.Parent);
        }

        public override string ToString() => $"[ {SubSelector}]";
    }
}
