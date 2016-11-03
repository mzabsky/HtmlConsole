using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IdSelector : Selector
    {
        public string Id { get; set; }

        public override bool Match(ElementNode node)
        {
            return node.Id == Id;
        }

        public override string ToString() => $"[#{Id}]";
    }
}
