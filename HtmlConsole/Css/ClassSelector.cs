using HtmlConsole.Dom;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    public class ClassSelector : Selector
    {
        public string Class { get; set; }

        public override SelectorMatch Match(ElementNode node)
        {
            var isSuccess = Class.IsIn(node.Classes);
            return new SelectorMatch(
                isSuccess,
                new Specificity
                {
                    ElementSpecificity = 0,
                    IdSpecificity = 0,
                    ClassSpecificity = isSuccess ? 1 : 0
                });
        }

        public override string ToString() => $"[.{Class}]";
    }
}
