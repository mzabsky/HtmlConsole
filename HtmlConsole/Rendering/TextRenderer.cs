using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class TextRenderer : Renderer
    {
        public TextRenderer(TextNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }
}