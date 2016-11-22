using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class ElementRenderer : Renderer
    {
        public ElementRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}