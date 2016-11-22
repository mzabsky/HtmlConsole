using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class InlineRenderer : ElementRenderer
    {
        public InlineRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}