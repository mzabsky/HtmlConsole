using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class ReplacedContentRenderer : Renderer
    {
        public ReplacedContentRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}