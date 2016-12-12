using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class BlockRenderer : ElementRenderer
    {
        public override bool IsBlock => true;
        public override bool IsInline => false;

        public BlockRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override void Paint(VisualLayer target)
        {
            foreach (var child in Children)
            {
                child.Paint(target);
            }
        }

        public override IRenderer Clone()
        {
            return new BlockRenderer((ElementNode)DomNode);
        }
    }
}