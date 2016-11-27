using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class TextRenderer : Renderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => true;

        public TextRenderer(TextNode domNode) : base(domNode)
        {
        }

        public override IRenderer Clone()
        {
            return new TextRenderer((TextNode)DomNode);
        }
    }
}