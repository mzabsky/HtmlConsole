using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public abstract class ElementRenderer : Renderer
    {
        protected ElementRenderer(ElementNode domNode) : base(domNode)
        {
        }
    }
}