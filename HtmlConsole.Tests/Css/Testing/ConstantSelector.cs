using HtmlConsole.Css;
using HtmlConsole.Dom;

namespace HtmlConsole.Tests.Css.Testing
{
    public class ConstantSelector : Selector
    {
        public bool Value { get; set; }

        public override bool Match(ElementNode node)
        {
            return Value;
        }
    }
}
