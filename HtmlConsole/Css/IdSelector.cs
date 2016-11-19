using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class IdSelector : Selector
    {
        public string Id { get; set; }

        public override SelectorMatch Match(ElementNode node)
        {
            var isSuccess = node.Id == Id;
            return new SelectorMatch(
                isSuccess,
                new Specificity(isSuccess ? 1 : 0, 0, 0)
            );
        }

        public override string ToString() => $"[#{Id}]";
    }
}
