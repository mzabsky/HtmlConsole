using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class ElementSelector : Selector
    {
        public string ElementName { get; set; }

        public override bool Match(ElementNode node)
        {
            return node.Element == ElementName;
        }

        public override string ToString() => $"[{ElementName}]";
    }
}
