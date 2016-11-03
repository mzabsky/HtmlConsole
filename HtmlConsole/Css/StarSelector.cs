using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class StarSelector : Selector
    {
        public override bool Match(ElementNode path)
        {
            return true;
        }

        public override string ToString() => $"[*]";
    }
}