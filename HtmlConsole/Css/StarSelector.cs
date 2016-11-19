using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class StarSelector : Selector
    {
        public override SelectorMatch Match(ElementNode path)
        {
            return new SelectorMatch(true, new Specificity());
        }

        public override string ToString() => $"[**]";
    }
}