using HtmlConsole.Css;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class TextRenderer : Renderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => true;

        private string Text => ((TextNode)DomNode).Text;

        public TextRenderer(TextNode domNode) : base(domNode)
        {
        }

        public override void Paint(VisualLayer target)
        {
            // TODO: Get proper z index
            target.Write(Position, Text, DomNode.Parent.GetStyleValue<ColorStyleValue>("color").Color, 0);
        }
        
        public override void Layout(LayoutContext context)
        {
            Position = context.Position;
            ClientSize = new Size(Text.Length, 1);
        }

        public override IRenderer Clone()
        {
            return new TextRenderer((TextNode)DomNode);
        }
    }
}