using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class InlineRenderer : ElementRenderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => true;

        public InlineRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override void Paint(VisualLayer target)
        {
            foreach (var child in Children)
            {
                child.Paint(target);
            }
        }

        public override void Layout(LayoutContext context)
        {
            Position = context.Position;

            var currentWidth = 0;
            var maximumHeight = 0;

            // Assuming all children are inlines here
            foreach (var child in Children)
            {
                child.Layout(new LayoutContext
                {
                    Position = context.Position + new Position(currentWidth, 0)
                });

                currentWidth += child.ClientSize.Width;

                if (child.ClientSize.Height > maximumHeight)
                {
                    maximumHeight = child.ClientSize.Height;
                }
            }

            ClientSize = new Size(currentWidth, maximumHeight);
        }

        public override IRenderer Clone()
        {
            return new InlineRenderer((ElementNode)DomNode);
        }
    }
}