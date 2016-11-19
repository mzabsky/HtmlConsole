using HtmlConsole.Css;
using HtmlConsole.Dom;

namespace HtmlConsole.Tests.Css.Testing
{
    public class ConstantSelector : Selector
    {
        public bool IsSuccess { get; set; }
        public Specificity Specificity { get; set; } = new Specificity();

        public override SelectorMatch Match(ElementNode node)
        {
            return new SelectorMatch(IsSuccess, Specificity);
        }
    }
}
