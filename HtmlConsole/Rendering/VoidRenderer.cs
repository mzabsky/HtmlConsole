using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class VoidRenderer : Renderer
    {
        public VoidRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}