using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class ElementSelector : Selector
    {
        public string ElementName { get; set; }

        public override SelectorMatch Match(ElementNode node)
        {
            var isSuccess = node.Element == ElementName;
            return new SelectorMatch(
                isSuccess, 
                new Specificity
                {
                    ElementSpecificity = isSuccess ? 1 : 0,
                    IdSpecificity = 0,
                    ClassSpecificity = 0
                });
        }

        public override string ToString() => $"[{ElementName}]";
    }
}
