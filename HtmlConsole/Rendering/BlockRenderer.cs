using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class BlockRenderer : ElementRenderer
    {
        public BlockRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}